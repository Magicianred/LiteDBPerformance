using System;
using System.Data;
using System.IO;
using Microsoft.Data.Sqlite;

namespace LiteDBPerformanceTester
{
    public class SQLiteTest : IDBTest
    {
        private string _filename;
        private SqliteConnection _db;
        private int _count;

        public int Count { get { return _count; } }
        public int FileLength { get { return (int)new FileInfo(_filename).Length; } }

        public SQLiteTest(int count, string password)
        {
            _count = count;
            _filename = "sqlite-" + Guid.NewGuid().ToString("n") + ".db";
            var cs = "Data Source=" + _filename;
            if (password != null) cs += "; Password=" + password;
            _db = new SqliteConnection(cs);
        }

        public void Prepare()
        {
            
            _db.Open();

            var table = new SqliteCommand("CREATE TABLE test_table (id INTEGER NOT NULL PRIMARY KEY, name TEXT, lorem TEXT)", _db);
            table.ExecuteNonQuery();

            var table2 = new SqliteCommand("CREATE TABLE test_table_bulk (id INTEGER NOT NULL PRIMARY KEY, name TEXT, lorem TEXT)", _db);
            table2.ExecuteNonQuery();
        }

        public void Insert()
        {
            var cmd = new SqliteCommand("INSERT INTO test_table (name, lorem) VALUES (@name, @lorem)", _db);

            cmd.Parameters.Add(new SqliteParameter("name", DbType.String));
            cmd.Parameters.Add(new SqliteParameter("lorem", DbType.String));

            foreach (var doc in Helper.GetDocs(_count))
            {
                cmd.Parameters["name"].Value = doc.Name;
                cmd.Parameters["lorem"].Value = doc.LongText;

                cmd.ExecuteNonQuery();
            }
        }

        public void Bulk()
        {
            using (var trans = _db.BeginTransaction())
            {
                var cmd = new SqliteCommand("INSERT INTO test_table_bulk (name, lorem) VALUES (@name, @lorem)", _db);

                cmd.Parameters.Add(new SqliteParameter("name", DbType.String));
                cmd.Parameters.Add(new SqliteParameter("lorem", DbType.String));

                foreach (var doc in Helper.GetDocs(_count))
                {
                    cmd.Parameters["name"].Value = doc.Name;
                    cmd.Parameters["lorem"].Value = doc.LongText;

                    cmd.Transaction = trans;
                    cmd.ExecuteNonQuery();
                }

                trans.Commit();
            }
        }

        public void Update()
        {
            var cmd = new SqliteCommand("UPDATE test_table SET name = @name, lorem = @lorem WHERE id = @id", _db);

            cmd.Parameters.Add(new SqliteParameter("id", DbType.Int32));
            cmd.Parameters.Add(new SqliteParameter("name", DbType.String));
            cmd.Parameters.Add(new SqliteParameter("lorem", DbType.String));

            foreach (var doc in Helper.GetDocs(_count))
            {
                cmd.Parameters["id"].Value = doc.Id + 1;
                cmd.Parameters["name"].Value = doc.Name;
                cmd.Parameters["lorem"].Value = doc.LongText;

                cmd.ExecuteNonQuery();
            }
        }

        public void CreateIndex()
        {
            var cmd = new SqliteCommand("CREATE INDEX idx1 ON test_table (name)", _db);

            cmd.ExecuteNonQuery();
        }

        public void Query()
        {
            var cmd = new SqliteCommand("SELECT * FROM test_table WHERE id = @id", _db);

            cmd.Parameters.Add(new SqliteParameter("id", DbType.Int32));

            for (var i = 1; i < _count; i++)
            {
                cmd.Parameters["id"].Value = i;

                var r = cmd.ExecuteReader();

                r.Read();

                var name = r.GetString(1);
                var lorem = r.GetString(2);

                r.Close();
            }
        }

        public void Delete()
        {
            var cmd = new SqliteCommand("DELETE FROM test_table", _db);

            cmd.ExecuteNonQuery();
        }

        public void Drop()
        {
            var cmd = new SqliteCommand("DROP TABLE test_table_bulk", _db);

            cmd.ExecuteNonQuery();
        }

        public void Dispose()
        {
            _db.Dispose();
            File.Delete(_filename);
        }
    }
}
