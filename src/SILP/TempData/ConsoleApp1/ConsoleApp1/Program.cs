using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleApp1
{
    class Program
    {

        public static string[] names = { "Ivan", "Petr"};
        public static string[] surnames = { "Ivanov", "Petrov"};

        public class PRISM
        {
            public List<DateTime> In { get; set; } = new List<DateTime>();
            public List<DateTime> Out { get; set; } = new List<DateTime>();
        }

        public struct EffortStruct
        {
            public double STD;
            public double OVR;
            public EffortStruct(double std, double ovr)
            {
                STD = std;
                OVR = ovr;
            }
        }

        public enum StatusEnum
        {
            OPEN,
            CLOSE
        }

        public class TimeReportsUnit
        {
            public string Project { get; set; }
            public string Task { get; set; }
            public string Issue { get; set; }
            public EffortStruct Effort { get; set; }
            public string Description { get; set; }
            public string BLB { get; set; }
            public string OVR { get; set; }
            public DateTime StartedDate { get; set; }
            public DateTime CompletionDate { get; set; }
            public StatusEnum Status { get; set; }
            public TimeReportsUnit
                (EffortStruct effort, string description, DateTime startedDate, DateTime completitionDate, StatusEnum status)
            {
                Project = "Internal";
                Task = "none";
                Issue = string.Empty;
                Effort = effort;
                Description = new string(description.ToCharArray());
                BLB = string.Empty;
                OVR = string.Empty;
                StartedDate = new DateTime(startedDate.ToBinary());
                CompletionDate = new DateTime(completitionDate.ToBinary());
                Status = status;
            }
        }

        public class TimeReports
        {
            public List<TimeReportsUnit> Reports { get; set; } = new List<TimeReportsUnit>();
        }

        public static void AddPRISMData(string folderPath)
        {
            Random r = new Random();
            string path = folderPath + "\\PRISM.txt";
            FileInfo file = new FileInfo(folderPath + "\\Time Reports.txt");
            file.Delete();
            List<PRISM> PRISM = new List<PRISM>();
            int[] months = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
            using (StreamWriter writerToFile = new StreamWriter(path))
            {
                foreach (string name in names)
                {
                    foreach (string surmane in surnames)
                    {
                        string fullName = name + " " + surmane;
                        for (int k = DateTime.Now.Year - 10; k < DateTime.Now.Year + 1; k++)
                        {
                            for (int j = 0; j < months.Length; j++)
                            {
                                for (int i = 1; i <= DateTime.DaysInMonth(k, months[j]); i++)
                                {
                                    DateTime currentDate = new DateTime(k, months[j], i, r.Next(23), r.Next(59), r.Next(59));
                                    if (currentDate.DayOfYear == DateTime.Now.DayOfYear && currentDate.Year == DateTime.Now.Year) goto label;
                                    if (currentDate.DayOfWeek == DayOfWeek.Saturday || currentDate.DayOfWeek == DayOfWeek.Sunday) continue;
                                    PRISM p = new PRISM();
                                    double[] normalRnd = new double[r.Next(8)];
                                    int flag = i;
                                    for (int l = 0; l < normalRnd.Length; l++)
                                    {
                                        normalRnd[l] = r.NextDouble() * normalRnd.Length;
                                        if (new DateTime(currentDate.ToBinary()).AddHours(normalRnd[l]).Day != flag) break;
                                        if (p.Out.Count == 0)
                                        {
                                            p.In.Add(new DateTime(currentDate.ToBinary()).AddHours(normalRnd[l]));
                                            p.Out.Add(new DateTime(currentDate.AddHours(normalRnd[l]).AddMinutes(r.Next(59)).ToBinary()));
                                        }
                                        else
                                        {
                                            if (new DateTime(p.Out[p.Out.Count - 1].ToBinary()).AddHours(normalRnd[l]).Day != flag) break;
                                            p.In.Add(new DateTime(p.Out[p.Out.Count - 1].ToBinary()).AddHours(normalRnd[l]));
                                            p.Out.Add(new DateTime(p.In[p.In.Count - 1].ToBinary()).AddMinutes(r.Next(59)));
                                        }
                                    }
                                    PRISM.Add(p);
                                }
                            }
                        }
                        label:
                        writerToFile.WriteLine("\"" + fullName + "\":");
                        writerToFile.WriteLine("\t[");
                        for (int i = 0; i < PRISM.Count; i++)
                        {
                            writerToFile.WriteLine("\t\t{");
                            writerToFile.WriteLine("\t\t\t\"in\": {");
                            for (int j = 0; j < PRISM[i].In.Count; j++)
                            {
                                if (j == PRISM[i].In.Count - 1)
                                    writerToFile.WriteLine("\t\t\t\t" + "\"" + PRISM[i].In[j] + "\"");
                                else
                                    writerToFile.WriteLine("\t\t\t\t" + "\"" + PRISM[i].In[j] + "\",");
                            }
                            writerToFile.WriteLine("\t\t\t}");
                            writerToFile.WriteLine("\t\t\t\"out\": {");
                            for (int j = 0; j < PRISM[i].Out.Count; j++)
                            {
                                if (j == PRISM[i].Out.Count - 1)
                                    writerToFile.WriteLine("\t\t\t\t" + "\"" + PRISM[i].Out[j] + "\"");
                                else
                                    writerToFile.WriteLine("\t\t\t\t" + "\"" + PRISM[i].Out[j] + "\",");
                            }
                            writerToFile.WriteLine("\t\t\t}");
                            writerToFile.WriteLine("\t\t}");
                        }
                        writerToFile.WriteLine("\t]");
                    }
                }
            }
            foreach (string name in names)
            {
                foreach (string surmane in surnames)
                {
                    string fullName = name + " " + surmane;
                    AddTimeReportsData(folderPath, PRISM, fullName);
                }
            }
        }

        public static void AddTimeReportsData(string folderPath, List<PRISM> PRISM, string fullName)
        {
            string path = folderPath + "\\Time Reports.txt";
            string[] s1 = { "Preparing", "for", "C#", "exam:", "studied", "dynamic", "types", "values", "and" };
            string[] s2 = { "references", "Updated", "Microsoft", "Visual", "Studio", "2015", "garbage", "collection" };
            string[] s3 = { "generics", "Created", "API", "for", "accessing", "to", "test", "data" };
            Random r = new Random();
            List<TimeReports> reports = new List<TimeReports>();
            for (int i = 0; i < PRISM.Count; i++)
            {
                if (PRISM[i].In.Count == 0) continue;
                DateTime startedDate = new DateTime(PRISM[i].In[0].ToBinary());
                TimeSpan timeForTask = PRISM[i].Out[PRISM[i].Out.Count - 1] - PRISM[i].In[0];
                int tasksCount = r.Next(15);
                TimeReports report = new TimeReports();
                for (int j = 0; j < tasksCount; j++)
                {
                    int t = r.Next(timeForTask.Hours * 60 + timeForTask.Minutes);
                    TimeSpan tt = timeForTask.Subtract(new TimeSpan(t/60, t%60, t - t/60 - t%60));
                    if (tt < timeForTask)
                    {
                        double time = Math.Round(t / 60 + ((double)(t % 60) / 60), 2) % 2;
                        report.Reports.Add(new TimeReportsUnit(new EffortStruct(time, 0), s1[r.Next(s1.Length - 1)] + " " + s2[r.Next(s2.Length - 1)] + " " + s3[r.Next(s3.Length - 1)], startedDate, startedDate + tt, r.Next(-1, 1) > 0 ? StatusEnum.CLOSE : StatusEnum.OPEN));
                    }
                    else break;
                }
                reports.Add(report);
            }
            FileInfo file = new FileInfo(path);
            StreamWriter writerToFile = file.AppendText();
            writerToFile.WriteLine("\"" + fullName + "\":");
            writerToFile.WriteLine("\t[");
            for (int i = 0; i < reports.Count; i++)
            {
                writerToFile.WriteLine("\t{");
                for (int j = 0; j < reports[i].Reports.Count; j++)
                {
                    writerToFile.WriteLine("\t\t\t\"Project\": " + "\"" + reports[i].Reports[j].Project + "\",");
                    writerToFile.WriteLine("\t\t\t\"Task\": " + "\"" + reports[i].Reports[j].Task + "\",");
                    writerToFile.WriteLine("\t\t\t\"Issue\": " + "\"" + reports[i].Reports[j].Issue + "\",");
                    writerToFile.WriteLine("\t\t\t\"Effort\": {");
                    writerToFile.WriteLine("\t\t\t\t\"STD\": " + reports[i].Reports[j].Effort.STD + "\",");
                    writerToFile.WriteLine("\t\t\t\t\"OVR\": " + reports[i].Reports[j].Effort.OVR + "\"");
                    writerToFile.WriteLine("\t\t\t}");
                    writerToFile.WriteLine("\t\t\t\"Description\": " + "\"" + reports[i].Reports[j].Description + "\",");
                    writerToFile.WriteLine("\t\t\t\"BLB\": " + "\"" + reports[i].Reports[j].BLB + "\",");
                    writerToFile.WriteLine("\t\t\t\"OVR\": " + "\"" + reports[i].Reports[j].OVR + "\",");
                    writerToFile.WriteLine("\t\t\t\"Started Date\": " + "\"" + reports[i].Reports[j].StartedDate + "\",");
                    writerToFile.WriteLine("\t\t\t\"Completition Date\": " + "\"" + reports[i].Reports[j].CompletionDate + "\",");
                    writerToFile.WriteLine("\t\t\t\"Status\": " + "\"" + reports[i].Reports[j].Status + "\"");
                    if (j == reports[i].Reports.Count - 1)
                        writerToFile.WriteLine("\t\t}");
                    else
                        writerToFile.WriteLine("\t\t},");
                }
            }
            writerToFile.WriteLine("\t]");
            writerToFile.Close();
        }

        static void Main(string[] args)
        {
            //C:\Users\bogdan.vetrenko\Source\Repos\SILP\src\SILP\TempData
            Console.WriteLine("Input path to folder for template files:");
            string path = Console.ReadLine();
            AddPRISMData(path);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Done!");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }
    }
}
