using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalSort
{
    class Bus
    {
        string bus, line, subLine, ticket, name, stop, run;
        int[] days = new int[7];
        int time;

        public Bus(string bus, string line, string subLine, string ticket, string name, string stop, string run, string time, string [] days)
        {
            this.bus = bus;
            this.line = line;
            this.subLine = subLine;
            this.ticket = ticket;
            this.name = name;
            this.stop = stop;
            this.run = run;
            int number;
            bool result = Int32.TryParse(time, out number);
            this.time = number;
            for (int i = 0; i < this.days.Length; i++)
            {
                result = Int32.TryParse(days[i], out number);
                this.days[i] = number;
            }
        }

       
        public string ToStringText()
        {
            string str = bus + "," + line + "," + subLine + "," + ticket + "," + name + "," + stop + "," + run;
            for (int i = 0; i < this.days.Length; i++)
            {   
                if (i == this.days.Length -1)
                    str = this.days[i].ToString() ;
                else
                    str = this.days[i].ToString() + ",";
            }
            return str;

        }
    }
}
