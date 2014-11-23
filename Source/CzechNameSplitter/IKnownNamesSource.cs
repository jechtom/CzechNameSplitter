using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CzechNameSplitter
{
    public interface IKnownNamesSource
    {
        IEnumerable<string> GetNames();
        IEnumerable<string> GetDegrees();
    }
}
