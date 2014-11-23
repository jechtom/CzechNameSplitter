using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CzechNameSplitter
{
    public class NameParser
    {
        HashSet<string> firstNames;
        HashSet<string> degrees;

        public NameParser(params IKnownNamesSource[] sources)
        {
            firstNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            degrees = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            foreach (var source in sources)
            {
                foreach (var name in source.GetNames())
                {
                    firstNames.Add(name.Trim().RemoveDiacritics());
                }

                foreach (var degree in source.GetDegrees())
                {
                    degrees.Add(degree.Trim().RemoveDiacritics());
                }
            }
        }

        public NameParser() : this(new DefaultKnownNamesSource()) { }

        public ParsedName Parse(string name)
        {
            var parts = name.Split(new char[] { ' ', ',' },  StringSplitOptions.RemoveEmptyEntries);
            var partTypes = new PartType[parts.Length];

            var result = new ParsedName()
            {
                OriginalName = name
            };
            
            for (int i = 0; i < parts.Length; i++)
            {
                var part = parts[i];
                var partWithoutDiacritism = part.RemoveDiacritics();
                if (IsFirstName(partWithoutDiacritism))
                {
                    partTypes[i] = PartType.FirstName;
                }
                else if (IsDegreeName(partWithoutDiacritism))
                {
                    partTypes[i] = PartType.Degree;
                }
                else
                {
                    partTypes[i] = PartType.Unknown;
                }
            }

            if(partTypes.Count(p => p == PartType.FirstName) == 1 && partTypes.Count(p => p == PartType.Unknown) == 1)
            {
                // best result
                result.Score = 1.0;
                result.FirstName = parts[Array.IndexOf(partTypes, PartType.FirstName)];
                result.LastName = parts[Array.IndexOf(partTypes, PartType.Unknown)];
            }
            else
            {

            }

            for (int i = 0; i < parts.Length; i++)
            {
                switch (partTypes[i])
                {
                    case PartType.Unknown:
                        break;
                    case PartType.FirstName:
                        break;
                    case PartType.LastName:
                        break;
                    case PartType.Degree: // degrees
                        if (result.Degree == null)
                            result.Degree += " ";
                        result.Degree += parts[i];
                        break;
                    default:
                        break;
                }
            }

            return result;
        }

        private bool IsDegreeName(string part)
        {           
            if(degrees.Contains(part))
                return true;

            if (degrees.Contains(part + "."))
                return true;

            return false;
        }

        private bool IsFirstName(string part)
        {
            if (firstNames.Contains(part))
                return true;

            return false;
        }

        enum PartType { Unknown, FirstName, LastName, Degree }
    }
}
