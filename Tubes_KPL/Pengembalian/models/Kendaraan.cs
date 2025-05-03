using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tubes_KPL.Pengembalian.models
{
    public class Kendaraan
    {
        public int Id { get; set; }
        public string Nama { get; set; }
        public string Tipe { get; set; }
        public bool IsRented { get; set; }

        public override string ToString()
        {
            return $"ID: {Id}, Nama: {Nama}, Tipe: {Tipe}, Status: {(IsRented ? "Disewa" : "Tersedia")}";
        }
    }
}
