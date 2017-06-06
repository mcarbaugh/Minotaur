using System;
using System.IO;

namespace Minotaur
{
    public class CSVPrinter : IDisposable
    {
        public CSVPrinter()
        {
            _filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            _fileName = "MinotaurLog.csv";

            string file = String.Format("{0}\\{1}", _filePath, _fileName);

            if(!File.Exists(file))
            {
                File.Create(file).Close();

                using (System.IO.StreamWriter fileWriter = new System.IO.StreamWriter(file, true))
                {
                    fileWriter.WriteLine("Date,Time of Day,Interaction,ParentClass,Hours,Minutes,Seconds");
                }
            }
        }

        public void Print(PerformanceSnapshot performanceSnapshot)
        {
            string newLine = string.Format("{0},{1},{2},{3},{4},{5},{6}",
                performanceSnapshot.StartDate,
                performanceSnapshot.StartTime,
                performanceSnapshot.Interaction,
                performanceSnapshot.ParentClass,
                performanceSnapshot.Hours,
                performanceSnapshot.Minutes,
                performanceSnapshot.Seconds);

            string file = String.Format("{0}\\{1}", _filePath, _fileName);
            using (System.IO.StreamWriter fileWriter = new System.IO.StreamWriter(file,true))
            {
                fileWriter.WriteLine(newLine);
            }
        }

        private string _filePath;
        private string _fileName;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {

        }
    }
}
