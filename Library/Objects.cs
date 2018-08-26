using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class Objects
    {
        public class Task
        {
            private string _sourceIP;
            private string _destinationPath;
            private int days;
            private TimeSpan duration;
            
            public Task(string sourceIP, string destinationPath, int days)
            {
                _sourceIP = sourceIP;
                _destinationPath = destinationPath;
                this.days = days;
            }

            public Task()
            {

            }

            private TimeSpan CalculateTimeToFullHour()
            {
                DateTime dt_now = DateTime.Now;

                int nextHour = 0;
                if (dt_now.Hour < 23) nextHour = dt_now.Hour + 1;          
                DateTime dt_next = new DateTime(dt_now.Year, dt_now.Month, dt_now.Day, nextHour, 0, 0);
                TimeSpan span = dt_next - dt_now;

                if (span.TotalMilliseconds<0)
                {
                    span = dt_now - dt_next;
                }

                return span;
            }

            public string GetDuration()
            {
                return duration.ToString();
            }
        }

        public class Log
        {
            public Log()
            {

            }

            public Log(string text, int type)
            {
                this.text = text;
                this.type = type;
            }

            public enum Type
            {
                debug = 0,
                info = 1,
                error = 2
            }

            public int type;
            public string text { get; set; }
        }
    }
}
