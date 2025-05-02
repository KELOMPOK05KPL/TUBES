using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fiturPengembalian
{
    public class Vehicle<T>
    {
        public T Type { get; set; }
        public System.DateTime RentDate { get; set; }
        public string Name { get; set; }

        public Vehicle(T type, System.DateTime rentDate, string name)
        {
            Type = type;
            RentDate = rentDate;
            Name = name;
        }
    }
}
