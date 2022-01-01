using System;
using System.Diagnostics;

namespace Groophy.CmdFunc
{
    public class CmdFunc
    {
        private Process Shell;

        public CmdFunc(bool justerror, string workingdir)
        {
            try
            {
                Shell = new Process();
                ProcessStartInfo si = new ProcessStartInfo("cmd.exe");
                si.Arguments = "/k";
                si.RedirectStandardInput = true;
                si.RedirectStandardOutput = true;
                si.RedirectStandardError = true;
                si.UseShellExecute = false;
                si.CreateNoWindow = true;
                //Control.CheckForIllegalCrossThreadCalls = false;
                si.WorkingDirectory = workingdir;
                Shell.StartInfo = si;
                Shell.OutputDataReceived += Shell_OutputDataReceived;
                Shell.ErrorDataReceived += Shell_ErrorDataReceived;
                Shell.Start();
                Shell.BeginErrorReadLine();
                Shell.BeginOutputReadLine();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
        }

        private void Shell_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            back += "\n" + e.Data;
        }

        private void Shell_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (nn)
            {
                nn = false;
            }
            else
            {
                if (start)
                {
                    if (e.Data.Length > 21)
                    {
                        if (e.Data == "End-Flag-ID-Null" || e.Data.Substring(e.Data.Length - 21, 21) == "echo End-Flag-ID-Null") start = false;
                        else back += "\n" + e.Data;
                    }
                    else
                    {
                        if (e.Data == "End-Flag-ID-Null") start = false;
                        else back += "\n" + e.Data;
                    }
                }
            }
        }

        bool nn = true;
        string back = string.Empty;
        bool start = false;
        public string Input(string command)
        {
            nn = true;
            start = true;
            Shell.StandardInput.WriteLine(command);
            Shell.StandardInput.WriteLine("echo End-Flag-ID-Null");
            while (start) { }
            string a = back;
            back = string.Empty;
            nn = true;
            return a;
        }
    }
}
