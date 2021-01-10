using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GridBeyondProject.Models
{
    public class Statistics
    {
        public List<MarketPriceDTO> MarketPriceData { get; set; }

        public MarketPriceDTO MostCheapTime { get; set; }
        public MarketPriceDTO MostExpensiveTime { get; set; }

        public Statistics(List<MarketPrice> marketPriceDataSet)
        {
            UpdateMarketPriceData(marketPriceDataSet);
            UpdateMostCheapTime();
            UpdateMostExpensiveTime();
        }

        private void UpdateMarketPriceData(List<MarketPrice> marketPriceDataSet)
        {
            MarketPriceData = marketPriceDataSet
                    .Select(x => new MarketPriceDTO { Time = x.Time, Price = x.Price })
                    .ToList();
        }

        private void UpdateMostCheapTime()
        {
            MostCheapTime = MarketPriceData.Aggregate((time1, time2) => time1.Price < time2.Price ? time1 : time2);
        }

        private void UpdateMostExpensiveTime()
        {
            MostExpensiveTime = MarketPriceData.Aggregate((time1, time2) => time1.Price > time2.Price ? time1 : time2);
        }
    }
}