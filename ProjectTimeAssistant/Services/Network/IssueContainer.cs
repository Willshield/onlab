using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTimeAssistant.Services.Network
{
        public class IssueContainer
        {
            public Issue[] issues { get; set; }
            public int total_count { get; set; }
            public int offset { get; set; }
            public int limit { get; set; }
        }

        public class Issue
        {
            public int id { get; set; }
            public ProjectOf project { get; set; }
            public Tracker tracker { get; set; }
            public Status status { get; set; }
            public Priority priority { get; set; }
            public Author author { get; set; }
            public string subject { get; set; }
            public string description { get; set; }
            public string start_date { get; set; }
            public int done_ratio { get; set; }
            public DateTime created_on { get; set; }
            public DateTime updated_on { get; set; }
        }

        public class ProjectOf
        {
            public int id { get; set; }
            public string name { get; set; }
        }

        public class Tracker
        {
            public int id { get; set; }
            public string name { get; set; }
        }

        public class Status
        {
            public int id { get; set; }
            public string name { get; set; }
        }

        public class Priority
        {
            public int id { get; set; }
            public string name { get; set; }
        }

        public class Author
        {
            public int id { get; set; }
            public string name { get; set; }
        }

}
