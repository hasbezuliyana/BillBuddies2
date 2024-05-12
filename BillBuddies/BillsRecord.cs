using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillBuddies
{
    public class BillsRecord
    {
        public DateTime DateRecorded { get; set; }
        public string? PersonName { get; set; }
        public string? Description { get; set; }
        public double TotalAmount { get; set; }
        public string? SplitMethod { get; set; }
    }
}
