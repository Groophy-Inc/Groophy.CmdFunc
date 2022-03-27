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
            if (endflag)
            {
                data += e.Data + "\n";
            }
        }

        private void Shell_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e.Data == input)
            {
                return;
            }
            try
            {
                if (e.Data.Split(new[] { "@echo " }, StringSplitOptions.None)[1] == "End-Flag-ID-Null")
                {
                    endflag = false;
                    return;
                }
            }
            catch { }
            try
            {
                if (e.Data.Substring(0, 16) == "End-Flag-ID-Null")
                {
                    return;
                }
            }
            catch { }

            if (endflag)
            {
                data += e.Data + "\n";
            }
        }

        bool endflag = false;
        string data = string.Empty;
        string input = string.Empty;
        public string Input(string command)
        {
            input = command;
            data = string.Empty;
            endflag = true;
            Shell.StandardInput.WriteLine(command);
            Shell.StandardInput.WriteLine("@echo End-Flag-ID-Null");
            while (endflag) { }

            string[] lines = data.Split(new[] { "\r", "\n", "\r\n" }, StringSplitOptions.None);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i < lines.Length; i++)
            {
                if (!string.IsNullOrEmpty(lines[i]))
                {
                    sb.Append(lines[i] + Environment.NewLine);
                }
            }
            endflag = false;
            return sb.ToString();
        }
    }
}
