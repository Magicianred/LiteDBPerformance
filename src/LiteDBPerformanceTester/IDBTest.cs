using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteDBPerformanceTester
{
    public interface IDBTest : IDisposable
    {
        int Count { get; }
        int FileLength { get; }

        void Prepare();
        void Insert();
        void Bulk();
        void Update();
        void CreateIndex();
        void Query();
        void Delete();
        void Drop();
    }
}
