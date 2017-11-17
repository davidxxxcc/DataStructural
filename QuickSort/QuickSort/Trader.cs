using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickSort
{
    class Trader
    {
        private string code, name;
        private int price, buy, sell;

        public Trader(string code, string name, string price, string buy, string sell)
        {
            this.code = code;
            this.name = name;
            this.price = checkInteger(price);
            this.buy = checkInteger(buy);
            this.sell = checkInteger(sell);    
        }

        private int checkInteger(string num)
        {
            int number;
            bool result = Int32.TryParse(num, out number);
            if (result)
                return number;
            else
                return -1;
        }
    }
}
