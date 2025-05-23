using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TyperacerFreeDataDownloader
{
    public class Row
    {
        public int Race { get; set; }
        public int Speed { get; set; }
        public decimal Accuracy { get; set; }
        public int Points {  get; set; }
        public string Place {  get; set; }
        public string Date { get; set; }
    }
}
