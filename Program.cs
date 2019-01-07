using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CsvToClockify
{
    class Program
    {
        static HttpClient client = new HttpClient();        

        static void Main(string[] args)
        {
            if (args.Length < 2)
                Console.WriteLine("Usage: CsvToClockify <api_key> <csv_input_file>");

            RunAsync(args[0], args[1]).GetAwaiter().GetResult();
        }

        static async Task<int> RunAsync(string api_key, string inputFile)
        {
            Dictionary<string, string> workspaceMap = new Dictionary<string, string>();
            Dictionary<string, string> projectMap = new Dictionary<string, string>();

            client.BaseAddress = new Uri("https://api.clockify.me/api/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("X-Api-Key", api_key);

            // Get all the workspaces
            var response = await client.GetAsync("workspaces/");
            if (response.IsSuccessStatusCode)
            {
                List<Workspace> workspaces = await response.Content.ReadAsAsync<List<Workspace>>();
                foreach(var workspace in workspaces)
                    workspaceMap.Add(workspace.name, workspace.id);
            }

            // Get all the projects
            foreach (var workspaceKvp in workspaceMap)
            {
                response = await client.GetAsync($"workspaces/{workspaceKvp.Value}/projects/");
                if (response.IsSuccessStatusCode)
                {
                    List<Project> projects = await response.Content.ReadAsAsync<List<Project>>();
                    foreach (var project in projects)
                        projectMap.Add(workspaceKvp.Key + project.client?.name + project.name, project.id);
                }
            }

            // Read the input file            
            using (TextFieldParser csvParser = new TextFieldParser(inputFile))
            {
                csvParser.CommentTokens = new string[] { "#" };
                csvParser.SetDelimiters(new string[] { "," });
                csvParser.HasFieldsEnclosedInQuotes = true;

                // Skip the row with the column names
                csvParser.ReadLine();

                // Start creating tasks
                while (!csvParser.EndOfData)
                {
                    // Read current line fields, pointer moves to the next line.
                    string[] fields = csvParser.ReadFields();

                    string test = projectMap.Keys.ElementAt(3);
                    string test2 = $"{fields[0]}{fields[1]}{fields[2]}";

                    // Create a time entry
                    TimeEntry timeEntry = new TimeEntry();
                    timeEntry.projectId = projectMap[$"{fields[0]}{fields[1]}{fields[2]}"];
                    timeEntry.start = Convert.ToDateTime($"{fields[4]} {fields[5]}").ToUniversalTime()
                         .ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'");
                    timeEntry.end = Convert.ToDateTime($"{fields[6]} {fields[7]}").ToUniversalTime()
                         .ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'");
                    timeEntry.billable = "true";
                    timeEntry.description = fields[3];
                    //timeEntry.taskId = "";
                    //timeEntry.tagIds = new List<string>{ "" };

                    response = await client.PostAsJsonAsync($"workspaces/{workspaceMap[fields[0]]}/timeEntries/", timeEntry);
                    response.EnsureSuccessStatusCode();
                }
            }

            return 1;
        }
    }
}
