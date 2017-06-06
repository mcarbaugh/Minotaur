using System;
using System.Diagnostics;
using System.Windows.Input;

namespace Minotaur
{
    public class MinotaurCommand : ICommand
    {
        private Action _execute;

        public MinotaurCommand(Action execute)
        {
            if (execute == null)
                throw new ArgumentNullException("Minotaur did not detect a method to execute.");

            _stopwatch = new Stopwatch();
            _execute = execute;
            _outputFormat = OutputFormat.Off;
        }

        public MinotaurCommand(Action execute, OutputFormat outputFormat)
        {
            if (execute == null)
                throw new ArgumentNullException("Minotaur did not detect a method to execute.");

            _stopwatch = new Stopwatch();
            _execute = execute;
            _outputFormat = outputFormat;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            try
            {
                if (_stopwatch == null)
                    _stopwatch = new Stopwatch();

                _parentClass = _execute.Method.DeclaringType.Name;
                _methodName = _execute.Method.Name;
                _startTime = DateTime.Now;
                _stopwatch.Start();
                _execute();
                _stopwatch.Stop();
            }
            catch
            {

            }
            finally
            {
                switch(_outputFormat)
                {
                    case OutputFormat.Console:
                        WriteToOutputWindow();
                        break;
                    case OutputFormat.File:
                        LogPerformanceSnapshot();
                        break;
                    case OutputFormat.Both:
                        LogPerformanceSnapshot();
                        WriteToOutputWindow();
                        break;
                    default:
                        break;
                }

                Stopwatch.Reset();
            }
        }

        private void LogPerformanceSnapshot()
        {
            try
            {
                using (CSVPrinter printer = new CSVPrinter())
                {
                    printer.Print(PerformanceSnapshot);
                }
            }
            catch
            {

            }
        }

        private void WriteToOutputWindow()
        {
            Console.WriteLine(string.Empty);
            Console.WriteLine("-- Minotaur Performance Log --");
            Console.WriteLine(string.Format("Parent Class: {0}", ParentClass));
            Console.WriteLine(string.Format("Interaction: {0}", MethodName));
            Console.WriteLine(string.Format("Start Time: {0}", StartTime.ToShortTimeString()));
            Console.WriteLine(string.Format("Elapsed Time (h): {0}", Hours));
            Console.WriteLine(string.Format("Elapsed Time (m): {0}", Minutes));
            Console.WriteLine(string.Format("Elapsed Time (s): {0}", Seconds));
        }

        public DateTime StartTime
        {
            get
            {
                return _startTime;
            }
        }
        public string ParentClass
        {
            get
            {
                return _parentClass;
            }
        }
        public string MethodName
        {
            get
            {
                return _methodName;
            }
        }
        public int Hours
        {
            get
            {
                if (Stopwatch == null)
                    return 0;
                else
                    return Stopwatch.Elapsed.Hours;
            }
        }
        public int Minutes
        {
            get
            {
                if (Stopwatch == null)
                    return 0;
                else
                    return Stopwatch.Elapsed.Minutes;
            }
        }
        public double Seconds
        {
            get
            {
                if (Stopwatch == null)
                    return 0;
                else
                    return Stopwatch.Elapsed.TotalSeconds;
            }
        }
        public Stopwatch Stopwatch
        {
            get
            {
                return _stopwatch;
            }
        }
        public PerformanceSnapshot PerformanceSnapshot
        {
            get
            {
                return new PerformanceSnapshot()
                {
                    StartDate = StartTime.ToShortDateString(),
                    StartTime = StartTime.ToShortTimeString(),
                    ParentClass = ParentClass,
                    Interaction = MethodName,
                    Hours = Stopwatch.Elapsed.Hours,
                    Minutes = Stopwatch.Elapsed.Minutes,
                    Seconds = Stopwatch.Elapsed.TotalSeconds,
                };
            }
        }

        private Stopwatch _stopwatch;
        private DateTime _startTime;
        private string _parentClass;
        private string _methodName;
        private OutputFormat _outputFormat;
    }
}
