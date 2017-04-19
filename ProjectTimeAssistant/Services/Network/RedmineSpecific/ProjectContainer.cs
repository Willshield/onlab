using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTimeAssistant.Services.Network
{

        public class ProjectContainer
        {
            public Project[] projects { get; set; }
            public int total_count { get; set; }
            public int offset { get; set; }
            public int limit { get; set; }
        }

        public class Project
        {
            public int id { get; set; }
            public string name { get; set; }
            public string identifier { get; set; }
            public string description { get; set; }
            public int status { get; set; }
            public DateTime created_on { get; set; }
            public DateTime updated_on { get; set; }
            public ProjectOf parent { get; set; }
        }


}
