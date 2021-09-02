using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace EFPractice.ProcessControl {
  class ProcessManager {
    private ProcessStartInfo _pStartInfo;
    private List<ProcessInfo> _processes = new List<ProcessInfo>();
    private string _processReadResult;

    internal ProcessManager() {
      _pStartInfo = new ProcessStartInfo();
    }

    public void KillProcessByPortNumber(int port) {
      GatherAllProcesses();
      GeneratePort(_processReadResult);
      if (_processes.Any(p => p.PortNumber == port)) {
        IEnumerable<ProcessInfo> filteredPort = _processes.Where(p => p.PortNumber == port);
        foreach (ProcessInfo portObj in filteredPort) {
          try {
            Console.ReadKey();
            KillProcessAtPort(portObj);
          }
          catch (NullReferenceException e) {
            Console.WriteLine($"Error: {e}");
          }
        }
      }
      else {
        Console.WriteLine("No process to kill!");
      }
    }

    private string GatherAllProcesses() {
      _pStartInfo.FileName = "netstat.exe";
      _pStartInfo.Arguments = "-a -n -o";
      _pStartInfo.WindowStyle = ProcessWindowStyle.Maximized;
      _pStartInfo.UseShellExecute = false;
      _pStartInfo.RedirectStandardInput = true;
      _pStartInfo.RedirectStandardOutput = true;
      _pStartInfo.RedirectStandardError = true;

      var process = new Process() {
        StartInfo = _pStartInfo
      };
      process.Start();

      var soStream = process.StandardOutput;
      _processReadResult = soStream.ReadToEnd();

      if (process.ExitCode != 0) throw new Exception("something broke");
      return _processReadResult;
    }

    private void GeneratePort(string output) {
      var lines = Regex.Split(output, "\r\n");
      foreach (var line in lines) {
        if (line.Trim().StartsWith("Proto"))
          continue;
        var parts = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

        var len = parts.Length;
        if (len > 2)
          _processes.Add(new ProcessInfo {
            Protocol = parts[0],
            PortNumber = int.Parse(parts[1].Split(':').Last()),
            PID = int.Parse(parts[len - 1])
          });
      }
    }

    private void KillProcessAtPort(ProcessInfo portObj) {
      Process.GetProcessById(portObj.PID).Kill(true);
      Process.GetProcessById(portObj.PID).WaitForExit();
      Console.WriteLine($"Port { portObj.Protocol } { portObj.PortNumber } at PID { portObj.PID} Killed.");
    }
  }
}
