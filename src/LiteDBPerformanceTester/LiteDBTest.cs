using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;
using LiteDB.Engine;

namespace LiteDBPerformanceTester
{
    public class LiteDBTest : IDBTest
    {
        private string _filename;
        private LiteDatabase  _liteDb;
        private int _count;
        private List<Helper.TestDoc> _docs;

        public int Count { get { return _count; } }
        public int FileLength { get { return (int)new FileInfo(_filename).Length; } }

        public LiteDBTest(int count, string password)
        {
            _count = count;
            _filename = "LiteDB-" + Guid.NewGuid().ToString("n") + ".db";

            if (string.IsNullOrEmpty(password))
            {
                var settings = new EngineSettings { Password = null, Filename = _filename };
                var db = new LiteEngine(settings);
                _liteDb = new LiteDatabase(db);   
            }
            else
            {
                var settings = new EngineSettings { Password = password, Filename = _filename };
                var db = new LiteEngine(settings);
                _liteDb = new LiteDatabase(db);   
            }
        }

        public void Prepare()
        {
            _docs = Helper.GetDocs(_count).ToList();
        }

        public void Insert()
        {
            foreach (var doc in _docs)
            {
                _liteDb.GetCollection<Helper.TestDoc>().Insert(doc);
            }
        }

        public void Bulk()
        {
            var docsNew = _docs;

            foreach (var item in docsNew)
            {
                item.Id += 10000;
            }
            
            _liteDb.GetCollection<Helper.TestDoc>().InsertBulk(docsNew);
        }

        public void Update()
        {
            foreach (var doc in _docs)
            {
                _liteDb.GetCollection<Helper.TestDoc>().Update(doc);
            }
        }

        public void CreateIndex()
        {
            _liteDb.GetCollection<Helper.TestDoc>().EnsureIndex(x => x.Name);
        }

        public void Query()
        {
            for (var i = 0; i < _count; i++)
            {
                _liteDb.GetCollection<Helper.TestDoc>().Query().Where(c => c.Id == i + 1).Single();
            }
        }

        public void Delete()
        {
            for (var i = 0; i < _count; i++)
            {
                _liteDb.GetCollection<Helper.TestDoc>().Delete(i + 1);
            }
        }

        public void Drop()
        {
            _liteDb.DropCollection("TestDoc");
        }

        public void Dispose()
        {
            _liteDb.Dispose();
            File.Delete(_filename);
        }
    }
}
