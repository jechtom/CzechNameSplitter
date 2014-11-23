using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CzechNameSplitter.App
{
    class Program
    {
        static void Main(string[] args)
        {
            var parser = new NameParser();
            var names = File.ReadAllLines(args[0]);
            var namesResult = new List<ParsedName>();
            foreach (var name in names)
            {
                var result = parser.Parse(name);
                namesResult.Add(result);
            }

            int total = names.Length;

            foreach (var item in namesResult.Where(i => i.Score < 1))
            {
                Console.WriteLine(item.OriginalName);
            }

            Console.WriteLine("---");

            foreach (var gr in namesResult.GroupBy(g => g.Score))
            {
                int cnt = gr.Count();
                var perc = (double)cnt / (double)total * 100.0;
                Console.WriteLine(string.Format("Score: {0} - {1} ({2:0.0}%)", gr.Key, cnt, perc));
            }

            Console.ReadLine();
        }
    }
}
