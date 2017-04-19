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

        ////TODO: check issues if dirty?
        //private bool dirty;
        //public bool Dirty
        //{
        //    get { return dirty; }
        //    set { dirty = value; }
        //}

        public List<Issue> Issues { get; set; }

        //public Project()
        //{
        //    //TODO: Init params

        //    issuelist = new List<Issue>();
        //}

        //TODO
        //public void addWorktime(Issue issue)
        //{
        //    issuelist.Add(issue);
        //}

        //public void clearWorktime()
        //{
        //    issuelist.Clear();
        //}

    }
}
