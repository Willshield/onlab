
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProjectTimeAssistant.Models
{
    public class WorkTime
    {

        public int WorkTimeID { get; set; }
        
        private DateTime? startTime;
        public DateTime? StartTime
        {
            get { return startTime; }
            set
            {
                startTime = value ?? throw new ArgumentNullException("Starttime must be set");
            }
        }

        public double Hours { get; set; }
        public string Comment { get; set; }

        private DateTime? finishTime;
        //[NotMapped] toDo: column remove workaround
        public DateTime? FinishTime
        {
            get { return finishTime; }
            set
            {
                if (value != null)
                {
                    if (value < StartTime)
                    {
                        throw new ArgumentException("Work must be started before finished");
                    }
                }

                finishTime = value;
            }
        }

        //navigációs
        public int IssueID { get; set; }
        public Issue Issue { get; set; }


        //public WorkTime()
        //{
        //    StartTime = new DateTime();
        //    StartTime = DateTime.Now;
        //    FinishTime = new DateTime();
        //    FinishTime = null;
        //}

    }
}
