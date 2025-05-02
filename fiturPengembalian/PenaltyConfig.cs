using System.Collections.Generic;
namespace fiturPengembalian
{
    public class PenaltyConfig
    {
        public int MaxReturnDays { get; set; }
        public Dictionary<string, int> PenaltyPerDay { get; set; }
    }
}