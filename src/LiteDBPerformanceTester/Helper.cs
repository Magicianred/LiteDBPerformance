using LiteDB;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLipsum.Core;

namespace LiteDBPerformanceTester
{
    static class Helper
    {
        public static void Run(this IDBTest test, string name, Action action)
        {
            var sw = new Stopwatch();

            sw.Start();
            action();
            sw.Stop();

            var time = sw.ElapsedMilliseconds.ToString().PadLeft(5, ' ');
            var seg = Math.Round(test.Count / sw.Elapsed.TotalSeconds).ToString().PadLeft(8, ' ');

            Console.WriteLine(name.PadRight(15, ' ') + ": " +
                              time + " ms - " +
                              seg + " records/second");
        }

        public static IEnumerable<TestDoc> GetDocs(int count)
        {
            for (var i = 0; i < count; i++)
            {
                yield return new TestDoc
                {
                    Name = Guid.NewGuid().ToString(),
                    LongText = LoremIpsum()
                };
            }
        }

        private static string LoremIpsum()
        {
            var lipsum = new LipsumGenerator();
            return lipsum.GenerateLipsum(1);
        }

        public class TestDoc
        {
            public int Id { get; set; }

            public string Name { get; set; }

            public string LongText { get; set; }
        }
    }
}