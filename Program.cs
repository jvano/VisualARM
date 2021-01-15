using System;
using System.Diagnostics;
using System.Net;
using System.Windows.Forms;

namespace Vano.Tools.Azure
{
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                ReRunProgram();

                return;
            }

            // For private deployments let's not enforce SSL validation
            ServicePointManager.ServerCertificateValidationCallback = ((sender, certificate, chain, sslPolicyErrors) =>
            {
                return true;
            });

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

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
