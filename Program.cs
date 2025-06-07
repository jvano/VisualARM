using System;
using System.Diagnostics;
using System.Net;
using System.Threading;
using System.Windows.Forms;

namespace Vano.Tools.Azure
{
    public static class Program
    {
        // Capture the synchronization context of the main thread
        internal static SynchronizationContext SyncContext => SynchronizationContext.Current ?? new WindowsFormsSynchronizationContext();

        [STAThread]
        public static void Main(string[] args)
        {
            if (args.Length == 0 && !Debugger.IsAttached)
            {
                ReRunProgram();

                return;
            }

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls13;

            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = e.ExceptionObject as Exception;
            if (ex != null)
            {
                Trace.WriteLine(e.ToString());
            }
        }

        private static void ReRunProgram()
        {
            Process process = new Process();
            process.StartInfo.FileName = Process.GetCurrentProcess().MainModule.FileName;
            process.StartInfo.Verb = "runas";
            process.StartInfo.Arguments = "/elevated";
            process.Start();
        }
    }
}
