using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WPFGridsBenchmark
{
    class Result
    {
        public Result(List<double> totalTimes, bool excludeFirstTest, bool notSupported = false)
        {
            if (notSupported)
            {
                NotSupported = true;
                return;
            }
            if (totalTimes.Count == 1)
            {
                Total = totalTimes[0];
                Average = totalTimes[0];
                StandardDeviation = totalTimes[0];
            }
            else
            {
                double sumDeviation = 0.0;
                int index = 0;
                int count = excludeFirstTest ? totalTimes.Count - 1 : totalTimes.Count;
                totalTimes.ForEach(item =>
                {
                    if (excludeFirstTest && index != 0 || !excludeFirstTest)
                    {
                        Total += item;
                    }
                    index++;
                });
                Average = Total / count;
                totalTimes.ForEach(item =>
                {
                    if (excludeFirstTest && index != 0 || !excludeFirstTest)
                    {
                        sumDeviation += Math.Pow(item - Average, 2);
                    }
                });
                StandardDeviation = Math.Round(Math.Sqrt(sumDeviation / count), 1);
            }
        }

        public double Total { get; set; }
        public double Average { get; set; }
        public double StandardDeviation { get; set; }

        public long MemoryUsed { get; set; }

        public bool NotSupported { get; set; }

        public override string ToString()
        {
            if (NotSupported)
            {
                return "Not Supported";
            }
            else
            {
                return string.Format("Average time: {0}ms, StandardDeviation: {1}ms, {2} memory: {3}KB",
                    Average, StandardDeviation, MemoryUsed > 0 ? "Increase" : "Decrease", MemoryUsed);
            }
        }
    }
}
