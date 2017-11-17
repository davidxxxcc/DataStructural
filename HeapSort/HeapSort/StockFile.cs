using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeapSort
{
    class StockFile
    {
        private Stock[] stocks;
        private int itemCount;

        public StockFile()
        {
            itemCount = 0;
            stocks = new Stock[10];
        }


        public int GetItemCount()
        {
            return itemCount;
        }

        public Stock[] GetStocks()
        {
            return stocks;
        }

        public Stock GetStock(int i)
        {
            return stocks[i];
        }
        public void AddStock(string date, string stockCode, string stockName, string traderCode, string traderName, string price, string buy, string sell)
        {   if (itemCount >= stocks.Length)
                Array.Resize(ref stocks, stocks.Length * 2);
            stocks[itemCount++] = new Stock(date, stockCode, stockName, traderCode, traderName, price, buy, sell);
        }

        public bool StockExists(string date, string stockCode, string stockName, string traderCode, string traderName, string price, string buy, string sell)
        {
            if (itemCount > 0)
            {
                for (int i = 0; i < itemCount; i++)
                {
                    if (date.CompareTo(stocks[i].GetDate()) == 0 && stockCode.CompareTo(stocks[i].GetStockCode()) == 0)
                        if (stockName.CompareTo(stocks[i].GetStockName()) == 0 && traderCode.CompareTo(stocks[i].GetTraderCode()) == 0)
                            if (traderName.CompareTo(stocks[i].GetTraderName()) == 0 && price.CompareTo(stocks[i].GetPrice()) == 0)
                                if (buy.CompareTo(stocks[i].GetBuy()) == 0 && sell.CompareTo(stocks[i].GetSell()) == 0)
                                    return true;
                }
                return false;

            }
            return false;
        }

    }
}
