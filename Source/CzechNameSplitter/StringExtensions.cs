using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CzechNameSplitter
{
    public static class StringExtensions
    {
        public static string RemoveDiacritics(this string text)
        {
            string stringFormD = text.Normalize(System.Text.NormalizationForm.FormD);
            System.Text.StringBuilder retVal = new System.Text.StringBuilder();
            for (int index = 0; index < stringFormD.Length; index++)
            {
                if (System.Globalization.CharUnicodeInfo.GetUnicodeCategory(stringFormD[index]) != System.Globalization.UnicodeCategory.NonSpacingMark)
                    retVal.Append(stringFormD[index]);
            }
            return retVal.ToString().Normalize(System.Text.NormalizationForm.FormC);
        }
    }
}
