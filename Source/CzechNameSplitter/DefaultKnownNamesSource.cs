using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CzechNameSplitter
{
    public class DefaultKnownNamesSource : IKnownNamesSource
    {
        public IEnumerable<string> GetNames()
        {
            using (var str = LoadResource("CzechNames.txt"))
            using(var strR = new StreamReader(str))
            {
                while (!strR.EndOfStream)
                {
                    var line = strR.ReadLine();
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    yield return line;
                }
            }

            using (var str = LoadResource("OtherNames.txt"))
            using (var strR = new StreamReader(str))
            {
                while (!strR.EndOfStream)
                {
                    var line = strR.ReadLine();
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    yield return line;
                }
            }
        }

        public IEnumerable<string> GetDegrees()
        {
            using (var str = LoadResource("CzechDegrees.txt"))
            using (var strR = new StreamReader(str))
            {
                while (!strR.EndOfStream)
                {
                    var line = strR.ReadLine();
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    yield return line;
                }
            }
        }

        private static Stream LoadResource(string name)
        {
            var assembly = Assembly.GetExecutingAssembly();
            return assembly.GetManifestResourceStream(string.Format("CzechNameSplitter.{0}", name));
        }
    }
}
