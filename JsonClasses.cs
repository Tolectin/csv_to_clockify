using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvToClockify
{
    public class JsonClasses
    {
    }

    public class HourlyRate
    {
        public string amount { get; set; }
        public string currency { get; set; }
    }

    public class Membership
    {
        public HourlyRate hourlyRate { get; set; }
        public string membershipStatus { get; set; }
        public string membershipType { get; set; }
        public string target { get; set; }
        public string userId { get; set; }
    }

    public class Round
    {
        public string minutes { get; set; }
        public string round { get; set; }
    }

    public class WorkspaceSettings
    {
        public string canSeeTimeSheet { get; set; }
        public string defaultBillableProjects { get; set; }
        public string forceDescription { get; set; }
        public string forceProjects { get; set; }
        public string forceTags { get; set; }
        public string forceTasks { get; set; }
        public string lockTimeEntries { get; set; }
        public string onlyAdminsCreateProject { get; set; }
        public string onlyAdminsSeeAllTimeEntries { get; set; }
        public string onlyAdminsSeeBillableRates { get; set; }
        public string onlyAdminsSeeDashboard { get; set; }
        public string onlyAdminsSeePublicProjectsEntries { get; set; }
        public string projectFavorites { get; set; }
        public string projectPickerSpecialFilter { get; set; }
        public Round round { get; set; }
        public string timeRoundingInReports { get; set; }
    }

    public class Workspace
    {
        public HourlyRate hourlyRate { get; set; }
        public string id { get; set; }
        public string imageUrl { get; set; }
        public List<Membership> memberships { get; set; }
        public string name { get; set; }
        public WorkspaceSettings workspaceSettings { get; set; }
    }

    public class Client
    {
        public string id { get; set; }
        public string name { get; set; }
        public string workspaceId { get; set; }
    }

    public class Estimate
    {
        public string estimate { get; set; }
        public string type { get; set; }
    }

    public class Task
    {
        public string assigneeId { get; set; }
        public string estimate { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string projectId { get; set; }
        public string status { get; set; }
    }

    public class Project
    {
        public string archived { get; set; }
        public string billable { get; set; }
        public Client client { get; set; }
        public string clientId { get; set; }
        public string color { get; set; }
        public Estimate estimate { get; set; }
        public HourlyRate hourlyRate { get; set; }
        public string id { get; set; }
        public List<Membership> memberships { get; set; }
        public string name { get; set; }
        public string @public { get; set; }
        public List<Task> tasks { get; set; }
        public string workspaceId { get; set; }
    }
    public class TimeEntry
    {
        public string start { get; set; }
        public string billable { get; set; }
        public string description { get; set; }
        public string projectId { get; set; }
        public string taskId { get; set; }
        public string end { get; set; }
        public List<string> tagIds { get; set; }
    }
}
