using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Library
{
    public class Types
    {
        [DebuggerDisplay("{name}", Name ="")]
        public class Task
        {
            public string name { get; set; }
            public string _sourceIP { get; set; }
            public string _destinationPath { get; set; }
            public int days { get; set; }
            public int type { get; set; }
            public TimeSpan duration { get; set; }


            public Task(string sourceIP, string destinationPath, int days, int type, string name)
            {
                _sourceIP = sourceIP;
                _destinationPath = destinationPath;
                this.days = days;
                this.type = type;
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
                if (nextHour == 0) dt_next.AddDays(1);

                TimeSpan span = dt_next - dt_now;

                if (span.TotalMilliseconds<0)
                {
                    span = dt_now - dt_next;
                }

                return span;
            }

            public string GetDuration()
            {
                duration = CalculateTimeToFullHour();
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
                debug,
                info,
                error
            }

            public int type { get; set; }
            public string text { get; set; }
        }
    }

    public static class Config
    {
        public static List<Types.Task> load()
        {
            List<Types.Task> Tasks = new List<Types.Task>();
            Tasks.Clear();
            XmlDocument xmlDoc = new XmlDocument();
            if (File.Exists(Directory.GetCurrentDirectory() + "\\config.xml"))
            {
                using (XmlReader reader = XmlReader.Create(Directory.GetCurrentDirectory() + "\\config.xml"))
                {
                    while (reader.Read())
                    {
                        if (reader.IsStartElement())
                        {
                            switch (reader.Name)
                            {
                                case "Task":
                                    Types.Task t = new Types.Task();

                                    for (int i = 0; i < 5; ++i)
                                    {
                                        reader.Read();

                                        switch (reader.Name)
                                        {
                                            case "Name":
                                                t.name = reader?.Value ?? String.Empty;
                                                break;

                                            case "DestinationPath":
                                                t._destinationPath = reader?.Value ?? String.Empty;
                                                break;

                                            case "SourceIP":
                                                t._sourceIP = reader?.Value ?? String.Empty;
                                                break;

                                            case "keepDays":
                                                t.days = reader.Value != null ? int.Parse(reader.Value) : 0;
                                                break;

                                            case "Type":
                                                t.type = reader.Value != null ? int.Parse(reader.Value) : 0;
                                                break;
                                        }
                                    }

                                    Tasks.Add(t);
                                    break;

                                default:
                                    Logger.Logs.Add(new Types.Log("Error while reading XML conifg. Unexpected data.", (int)Types.Log.Type.error));
                                    reader.Close();
                                    break;

                            }
                        }
                    }
                }
            }
            else
            {
                using (XmlWriter xwriter = XmlWriter.Create(Directory.GetCurrentDirectory() + "\\config.xml"))
                {
                    xwriter.WriteStartDocument();
                    xwriter.WriteStartElement("Config");
                    xwriter.WriteStartElement("Tasks");
                    xwriter.WriteEndElement();
                    xwriter.WriteEndElement();
                    xwriter.WriteEndDocument();
                }
            }
            return Tasks;
        }

        public static bool save(List<Types.Task> Tasks)
        {
            using (XmlWriter xwriter = XmlWriter.Create(Directory.GetCurrentDirectory() + "\\config.xml"))
            {
                xwriter.WriteStartDocument();
                xwriter.WriteStartElement("Config");
                xwriter.WriteStartElement("Tasks");

                foreach (Types.Task t in Tasks)
                {
                    xwriter.WriteStartElement("Task");

                    xwriter.WriteElementString("Name", t.name);
                    xwriter.WriteElementString("DestinationPath", t._destinationPath);
                    xwriter.WriteElementString("SourceIP", t._sourceIP);
                    xwriter.WriteElementString("keepDays", t.days.ToString());
                    xwriter.WriteElementString("Type", t.type.ToString());

                    xwriter.WriteEndElement();
                }

                xwriter.WriteEndElement();
                xwriter.WriteEndElement();
                xwriter.WriteEndDocument();
            }
            return true;
        }
    }
}
