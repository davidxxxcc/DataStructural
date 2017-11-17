using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeapSort
{
    class Stock
    {
        private string date, stockCode, stockName, traderCode, traderName;
        private int buy, sell;
        private double price;
        public Stock()
        {

        }
        public Stock(string date, string stockCode, string stockName, string traderCode, string traderName, string price, string buy, string sell)
        {
            this.date = date;
            this.stockCode = stockCode;
            this.stockName = stockName;
            this.traderCode = traderCode;
            this.traderName = traderName;
            this.price = checkDouble(price);
            this.buy = checkInteger(buy);
            this.sell = checkInteger(sell);
        }

        public string GetDate()
        {
            return date;
        }

        public string GetStockCode()
        {
            return stockCode;
        }

        public string GetStockName()
        {
            return stockName;
        }

        public string GetTraderCode()
        {
            return traderCode;
        }

        public string GetTraderName()
        {
            return traderName;
        }

        public string GetPrice()
        {
            return price.ToString();
        }

        public double GetDoublePrice()
        {
            return price;
        }

        public string GetBuy()
        {
            return buy.ToString();
        }

        public string GetSell()
        {
            return sell.ToString();
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
        private double checkDouble(string num)
        {
            double number;
            bool result = Double.TryParse(num, out number);
            if (result)
                return number;
            else
                return -1;
        }

        override
        public string ToString()
        {
            string str = date + "," + stockCode + "," + stockName + "," + traderCode + "," + traderName + "," + price.ToString() + "," + buy.ToString() + "," + sell.ToString();
            return str;
        }

    }
}
