﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LiteDB;
using Ptilopsis.PtiApplication;
using Ptilopsis.PtiTask;

namespace Ptilopsis.PtiDB
{
    class LiteDbDataBase : IDataBase
    {
        public LiteDbDataBase()
        {
        }
        public override bool AddOrUpdateApp(PtiApp app)
        {
            if (string.IsNullOrWhiteSpace(app.Id))
            {
                return false;
            }
            else
            {
                app._id = app.Id;
            }
            using (var db = new LiteDatabase(Config.Get().LiteDBFile))
            {
                var col = db.GetCollection<PtiApp>("apps");
                col.Upsert(app._id, app);
            }
            return true;
        }
        public override PtiApp GetAppById(string id)
        {
            using (var db = new LiteDatabase(Config.Get().LiteDBFile))
            {
                var col = db.GetCollection<PtiApp>("apps");
                return col.FindById(id);
            }
        }

        public override PtiApp[] GetApps(int skip, int take)
        {
            using (var db = new LiteDatabase(Config.Get().LiteDBFile))
            {
                var col = db.GetCollection<PtiApp>("apps");
                return col.Find(q => true, skip, take).ToArray();
            }
        }

        public override bool AddOrUpdateTask(PtiTasker task)
        {
            if (string.IsNullOrWhiteSpace(task.Id))
            {
                return false;
            }
            else
            {
                task._id = task.Id;
            }
            using (var db = new LiteDatabase(Config.Get().LiteDBFile))
            {
                var col = db.GetCollection<PtiTasker>("tasks");
                col.Upsert(task._id, task);
            }
            return true;
        }

        public override bool DeleteApp(string id)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteTask(string id)
        {
            throw new NotImplementedException();
        }

        public override PtiTasker GetTask(string id)
        {
            using (var db = new LiteDatabase(Config.Get().LiteDBFile))
            {
                var col = db.GetCollection<PtiTasker>("apps");
                return col.FindById(id);
            }
        }

        public override PtiTasker[] GetTasks(int skip, int take)
        {
            using (var db = new LiteDatabase(Config.Get().LiteDBFile))
            {
                var col = db.GetCollection<PtiTasker>("tasks");
                return col.Find(q => true, skip, take).ToArray();
            }
        }

        public override PtiApp[] SearchAppByName(string name)
        {
            throw new NotImplementedException();
        }

        public override PtiTasker[] SearchTaskByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
