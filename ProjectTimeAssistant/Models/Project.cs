using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTimeAssistant.Models
{
    public class Project
    {

        public int ProjectID { get; set; }

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private string description;
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        //todo:Subprojects
        public List<Issue> Issues { get; set; }


    }
}
