﻿using Ptilopsis.Model;
using Ptilopsis.PtiTask;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ptilopsis.PtiRun
{
    public class RunnerManager:IWorker
    {
        List<PtiRunner> RunnerList { get; set; }
        public RunnerManager()
        {
            this.RunnerList = new List<PtiRunner>();
        }
        public bool CreateRunner(PtiRunTask runTask)
        {
            if (string.IsNullOrWhiteSpace(runTask.PtiTasker.RunCmd))
            {
                runTask.PtiTasker.RunCmd = runTask.PtiApp.DefaultRunCmd;
            }
            PtiRunner runner = new PtiRunner(runTask.PtiTasker, runTask.PtiApp);
            this.RunnerList.Add(runner);
            runTask.Runner = runner;
            return true;
        }
        public bool CreateAndStart(PtiRunTask runTask)
        {
            var _old = this.RunnerList.Where(r => r.TaskInfo.Id == runTask.PtiTasker.Id).FirstOrDefault();
            if (!runTask.PtiTasker.MultiRunner&&_old != null&& _old?.State==ProcessState.RUNNING)
            {
                return true;
            }
            this.CreateRunner(runTask);
            runTask.LastRunDate = DateTime.Now;
            runTask.PtiTasker.LastRunDate = runTask.LastRunDate;
            runTask.PtiApp.LastRunDate = runTask.PtiTasker.LastRunDate;
            runTask.Logger = runTask.Runner.Logger;
            runTask.Runner.Run();
            return true;
        }

        public bool CheckTaskAndKill(PtiRunTask runTask)
        {
            var _old = this.RunnerList.Where(r => r.TaskInfo.Id == runTask.PtiTasker.Id).FirstOrDefault();
            if (_old != null && _old?.State == ProcessState.RUNNING)
            {
                try
                {
                    _old.KillAsync();
                    return true;
                }
                catch(Exception ex)
                {
                    this.WriteError(ex.ToString());
                    return false;
                }
            }
            return true;
        }

        public bool RemoveRunner(PtiRunner runner)
        {
            try
            {
                this.RunnerList.Remove(runner);
            }
            catch (Exception ex)
            {
                this.WriteError(ex.ToString());
                return false;
            }
            return true;
        }
        #region 单例模式
        private static RunnerManager _runnerManager = null;
        public static RunnerManager Get()
        {
            if (_runnerManager == null)
            {
                _runnerManager = new RunnerManager();
            }
            return _runnerManager;
        }
        #endregion
    }
}
