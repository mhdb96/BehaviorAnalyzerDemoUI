using BADLibrary.Models;
using System;
using System.Collections.Generic;
using SysProcess = System.Diagnostics.Process;
using System.Text;

namespace BADLibrary.Managers
{
    public class ProcessManager
    {
        private static readonly ProcessManager _instance = new ProcessManager();
        List<Process> processes;
        private ProcessManager()
        {
            processes = new List<Process>();
        }
        public static ProcessManager GetProcessManager()
        {
            return _instance;
        }
        public List<Process> GetAllProcessData()
        {            
            SysProcess[] processCollection = SysProcess.GetProcesses();

            foreach (SysProcess p in processCollection)
            {
                try
                {
                    Process pro = new Process();
                    pro.Name = p.ProcessName;
                    pro.Id = p.Id;
                    processes.Add(pro);
                }
                catch (Exception)
                {
                    Console.WriteLine("no permession");
                }
                
            }            
            return processes;
        }
    }
}
