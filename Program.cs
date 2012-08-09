using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace PerformanceMatrix
{
    class Program
    {
        static void Main(string[] args)
        {
            //FileRead.Test();
            FileMatch.Test();

            Console.Read();
        }
    }

    class TimeTick : IDisposable
    {
        private Stopwatch timeTraker;

        public TimeSpan TimeElapsed { get; private set; }

        public TimeTick(string matter)
        {
            timeTraker = new Stopwatch();
            timeTraker.Start();
            Console.Write(Environment.NewLine);
            Console.WriteLine(matter);
        }

        public void Stop(long count)
        {
            timeTraker.Stop();
            TimeElapsed = timeTraker.Elapsed;
            Console.WriteLine("Match Count:\t\t" + count + " Items");
            Console.WriteLine("Time Taken (Minutes):\t" + TimeElapsed.Minutes);
            Console.WriteLine("Time Taken (seconds):\t" + TimeElapsed.Seconds);
            Console.WriteLine("Time Taken (MS):\t"+ TimeElapsed.Milliseconds);
        }

        public void Dispose()
        {
            timeTraker = null;
        }
    }
}
