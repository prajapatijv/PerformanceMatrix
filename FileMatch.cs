using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace PerformanceMatrix
{
    public static class FileMatch
    {
        private static List<Item> LeftItems;
        private static List<Item> RightItems;

        public static void Test()
        {
            int sampleCount = 100000;
            Console.WriteLine(string.Format("Compare List Of {0}  strings with List of {1} strings", sampleCount, sampleCount * 3));

            LoadItems(sampleCount);
            FileMatch1(sampleCount);
            FileMatch2(sampleCount);
        }

        static void LoadItems(int count)
        {
            Random rnd = new Random();
            LeftItems = Enumerable.Range(1, count)
                                  .Select(x => new Item { Name = "string-" + x.ToString() })
                                  .ToList();


            RightItems = Enumerable.Range(1, count * 3)
                                   .Select(x => new { val = x, order = rnd.Next() })
                                   .OrderBy(o => o.order)
                                   .Select(x => new Item { Name = "string-" + x.val })
                                   .ToList();

        }

        static void FileMatch1(int count)
        {
            var comparer = new LambdaComparer<Item>((x,y)=>x.Name == y.Name);
            using (var traker = new TimeTick(string.Format("Test1 - Using Intersect ", count, count * 3)))
            {
                var items = LeftItems.Intersect(RightItems, comparer);
                traker.Stop(items.Count());
            }
        }

        static void FileMatch2(int count)
        {
            using (var traker = new TimeTick(string.Format("Test2 - Using LINQ Join", count, count * 3)))
            {
                var queryLeft = from l in LeftItems
                        join r in RightItems on l.Name equals r.Name into temp
                        from r in temp.DefaultIfEmpty()
                        select new Item { Name = l.Name, Match = !(r == null) };

                var cnt = queryLeft.Where(x => x.Match == true).Count();
                traker.Stop(cnt);
            }
        }
    }

    class Item
    {
        public string Name { get; set; }
        public bool Match { get; set; }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
