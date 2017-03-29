
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProjectTimeAssistant.Model
{
    public class WorkTime
    {

        private Guid id;
        public Guid ID
        {
            get { return id; }
        }


        private DateTime? startTime;
        public DateTime? StartTime
        {
            get { return startTime; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Starttime must be set");
                }
                else if (value > FinishTime)
                {
                    throw new ArgumentException("Work must be started before finished");
                }

                startTime = value;
            }
        }


        private DateTime? finishTime;
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



        public WorkTime()
        {
            //id = new Guid("dddd-dddd");
            id = Guid.NewGuid();
            StartTime = new DateTime();
            StartTime = DateTime.Now;

            FinishTime = new DateTime();
            FinishTime = null;
        }

    }
}
