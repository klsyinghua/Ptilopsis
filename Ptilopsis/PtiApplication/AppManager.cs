﻿using Ptilopsis.Model;
using Ptilopsis.PtiDB;
using Ptilopsis.PtiEvent;
using Ptilopsis.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace Ptilopsis.PtiApplication
{
    public class AppManager: IWorker
    {
        IDataBase Db { get; set; }
        public TimeSpan CleanerInterval { get; set; }
        private AppManager()
        {
            this.Db = DBManager.Get().GetNewDb();
            this.CleanerInterval = TimeSpan.FromSeconds(10);
        }
        public override bool Start()
        {
            if (base.Start())
            {
                this.Db = DBManager.Get().GetNewDb();
                EventManager.Get().RegLoopEvent(this.Cleaner, PtiEventType.CheckAllTasks, this.CleanerInterval);
                return true;
            }
            return false;
        }
        /// <summary>
        /// App Cleaner 定时清理已删除的APP（清除时先判断是否有对应任务在运行）TODO
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public object Cleaner(PtiEventer value)
        {
            WriteInfo("AppCleaner Running");
            return true;
        }
        public bool AddApp(PtiApp app)
        {
            if (!this.CheckApp(app))
            {
                return false;
            }
            //解压文件
            string source = Path.Combine(Config.Get().AppZipPath, app.ZipFile);
            string targetPath = Path.Combine(Config.Get().AppRunPath, app.Id);
            if(!ZipHelper.UnZip(source, targetPath))
            {
                return false;
            }
            this.Db.AddOrUpdateApp(app);
            return true;
        }
        public bool DeleteApp(string id)
        {
            return this.Db.DeleteApp(id);
        }
        public bool CheckApp(PtiApp app)
        {
            try
            {
                if (string.IsNullOrEmpty(app.ZipFile))
                {
                    return false;
                }

                if (!File.Exists(Path.Combine(Config.Get().AppZipPath, app.ZipFile)))
                {
                    return false;
                }
                if (string.IsNullOrWhiteSpace(app.Name))
                {
                    return false;
                }
                if (string.IsNullOrWhiteSpace(app.Id))
                {
                    app.Id = MD5Helper.getMd5Hash(app.Name);
                }
                app.UpdateDate = DateTime.Now;
                if (app.CreateDate == null)
                {
                    app.CreateDate = app.UpdateDate;
                }
                return true;
            }
            catch (Exception e)
            {
                WriteError(e.ToString());
                return false;
            }
        }
        /// <summary>
        /// 获得所有APP
        /// </summary>
        /// <param name="callback"></param>
        /// <returns></returns>
        public PtiApp[] GetAllApps()
        {
            try
            {
                return DBManager.Get().GetAllApps(this.Db).ToArray();
            }
            catch(Exception e)
            {
                WriteError(e.ToString());
                return new PtiApp[] { };
            }
        }

        #region 单例模式
        private static AppManager _appManager = null;
        public static AppManager Get()
        {
            if (_appManager == null)
            {
                _appManager = new AppManager();
            }
            return _appManager;
        }
        #endregion
    }
}
