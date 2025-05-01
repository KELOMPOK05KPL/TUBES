using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tubes_KPL.Utilities
{
    public static class TableDrivenHelper
    {
        // Code Reuse: Method generik untuk operasi table-driven
        public static T GetFromTable<T>(Dictionary<string, T> table, string key, string errorMessage)
        {
            if (table.TryGetValue(key, out T value))
            {
                return value;
            }
            throw new Exception(errorMessage);
        }

        public static void UpdateInTable<T>(Dictionary<string, T> table, string key, T value, string errorMessage)
        {
            if (table.ContainsKey(key))
            {
                table[key] = value;
            }
            else
            {
                throw new Exception(errorMessage);
            }
        }

        public static void DeleteFromTable<T>(Dictionary<string, T> table, string key, string errorMessage)
        {
            if (!table.Remove(key))
            {
                throw new Exception(errorMessage);
            }
        }
    }
}
