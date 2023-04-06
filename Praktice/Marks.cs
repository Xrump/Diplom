using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praktice
{
    public class Marks
    {
        public Marks(string header, List<decimal> mark)
        {
            Header = header;
            Mark = mark;
        }

        public Marks(string name, string header, List<decimal> mark)
        {
            Name = name;
            Header = header;
            Mark = mark;
        }

        public string Name { get; set; }
        public string Header { get; set; }
        public List<decimal> Mark { get; set; }
        public decimal AverageMark
        {
            get
            {
                decimal summ = 0;

                foreach (var number in Mark)
                    summ += number;

                return Math.Round(summ / Mark.Count, 2);
            }
        }

    }
}
