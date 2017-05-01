using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTimeAssistant.Services.Network
{

        public class TimeEntriesContainer
        {
            public Time_Entries[] time_entries { get; set; }
            public int total_count { get; set; }
            public int offset { get; set; }
            public int limit { get; set; }
        }

        public class Time_Entries
        {
            public int id { get; set; }
            public ProjectOf project { get; set; }
            public IssueOf issue { get; set; }
            public User user { get; set; }
            public Activity activity { get; set; }
            public float hours { get; set; }
            public string comments { get; set; }
            public string spent_on { get; set; }
            public DateTime created_on { get; set; }
            public DateTime updated_on { get; set; }
        }

        public class IssueOf
        {
            public int id { get; set; }
        }

        public class User
        {
            public int id { get; set; }
            public string name { get; set; }
        }

        public class Activity
        {
            public int id { get; set; }
            public string name { get; set; }
        }

    public class Time_Entry
    {
        public string key { get; set; }
        public int issue_id { get; set; }
        public DateTime spent_on { get; set; }
        public float hours { get; set; }
        public int activity_id { get; set; }
        public string comments { get; set; }
    }

}
