using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;

namespace WPFGridsBenchmark
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private TextWriterTraceListener _logListener;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            // Uncomment below lines if you need to get log file.
            //Stream logFile = File.Create("WPFDataGridsTest" + DateTime.UtcNow.ToString("yyyyMMddTHHmmss") + ".txt");

            //_logListener = new TextWriterTraceListener(logFile);
            //Trace.Listeners.Add(_logListener);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            if (_logListener != null)
            {
                _logListener.Flush();
            }
            base.OnExit(e);
        }
    }
}
