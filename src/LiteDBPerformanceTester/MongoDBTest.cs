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
using MongoDB.Driver;

namespace LiteDBPerformanceTester
{
    public class MongoDBTest : IDBTest
    {
        public virtual string ConnectionString { get { return "mongodb+srv://testuser:testpassword@cluster0.cfygw.mongodb.net/test?retryWrites=true&w=majority"; } }
        private IMongoDatabase _db;
        private IMongoCollection<Helper.TestDoc> _collection;

        private int _count;
        private List<Helper.TestDoc> _docs;

        public int Count { get { return _count; } }
        public int FileLength { get { return (0); } }

        public MongoDBTest(int count, string password)
        {
            _count = count;

            var client = new MongoClient(this.ConnectionString);

            client.DropDatabase("test");

            _db = client.GetDatabase("test");
            _db.DropCollection("docs");

            _collection = _db.GetCollection<Helper.TestDoc>("docs");
        }

        public void Prepare()
        {
            _docs = Helper.GetDocs(_count).ToList();
        }

        public void Insert()
        {
            var i = 1;
            foreach (var doc in _docs)
            {
                doc.Id = i;
                _collection.InsertOne(doc);
                i++;
            }
        }

        public void Bulk()
        {
            var i = 1;
            foreach (var doc in _docs)
            {
                doc.Id = i + _docs.Count;
                i++;
            }

            _collection.InsertMany(_docs);
        }

        public void Update()
        {
            foreach (var doc in _docs)
            {
                var filter = Builders<Helper.TestDoc>.Filter.Where(x => x.Id == doc.Id);
                var update = Builders<Helper.TestDoc>.Update.Set(x => x.Name, "update");
                _collection.UpdateOne(filter, update);
            }
        }

        public void CreateIndex()
        {
            var indexKeysDefinition = Builders<Helper.TestDoc>.IndexKeys.Ascending(c => c.Name);
            _collection.Indexes.CreateOne(new CreateIndexModel<Helper.TestDoc>(indexKeysDefinition));
        }

        public void Query()
        {
            for (var i = 0; i < _count; i++)
            {
                var doc = _collection.Find(x => x.Id == i + 1).FirstOrDefault();
            }
        }

        public void Delete()
        {
            for (var i = 0; i < _count; i++)
            {
                _collection.DeleteOne(a => a.Id == i + 1);
            }
        }

        public void Drop()
        {
            _db.DropCollection("docs");
        }

        public void Dispose()
        {
            _db = null;
            _collection = null;
        }
    }
}
