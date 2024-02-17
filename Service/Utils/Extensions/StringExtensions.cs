using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegraHub.Service.Utils.Extensions
{
    public static class StringExtensions
    {
        public static string AppendIfNotPresent(this string source, string toAppend)
        {
            return source.Contains(toAppend) ? source : source + toAppend;
        }

        public static string RemoveIfPresent(this string source, string toCut)
        {
            return source.Contains(toCut) ? source.Replace(toCut, "") : source;
        }

    }
}
