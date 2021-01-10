using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GridBeyondProject.Models
{
    public class HourWindow
    {
        public MarketPriceDTO StartWindow { get; set; }
        public MarketPriceDTO EndWindow { get; set; }

        public decimal Cost;
    }

    public class Statistics
    {
        public List<MarketPriceDTO> MarketPriceDataSumByDay { get; set; }
        public List<List<MarketPriceDTO>> MarketPriceDay { get; set; }

        public MarketPriceDTO MostCheapTime { get; set; }
        public MarketPriceDTO MostExpensiveTime { get; set; }
        public decimal AverageCost { get; set; }

        public HourWindow MostExpensiveHourWindow;

        public Statistics(List<MarketPrice> marketPriceDataSet)
        {
            UpdateStatistics(marketPriceDataSet);
        }

        public void UpdateStatistics(List<MarketPrice> marketPriceDataSet)
        {
            UpdateMarketPriceDataSumByDay(marketPriceDataSet);
            UpdateMarketPriceDay(marketPriceDataSet);
            UpdateMostCheapTime(marketPriceDataSet);
            UpdateMostExpensiveTime(marketPriceDataSet);
            UpdateAverageCost(marketPriceDataSet);
            UpdateMostExpensiveHourWindow(marketPriceDataSet);
        }

        private void UpdateMarketPriceDataSumByDay(List<MarketPrice> marketPriceDataSet)
        {
            MarketPriceDataSumByDay = marketPriceDataSet
                .GroupBy(x => new DateTime(x.Time.Year, x.Time.Month, x.Time.Day))
                .Select(g => new MarketPriceDTO { Time = g.Key, ShortDateString = g.Key.ToShortDateString(), Price = g.Sum(mk => mk.Price) })
                .ToList();
        }

        private List<MarketPriceDTO> GetMarketPriceOfDay(List<MarketPrice> marketPriceDataSet, DateTime date)
        {
            return marketPriceDataSet
                    .Where(x => new DateTime(x.Time.Year, x.Time.Month, x.Time.Day) == date)
                    .Select(x => new MarketPriceDTO { Time = x.Time, ShortDateString = x.Time.ToShortDateString(), Price = x.Price })
                    .ToList();
        }

        private void UpdateMarketPriceDay(List<MarketPrice> marketPriceDataSet)
        {
            MarketPriceDay = new List<List<MarketPriceDTO>>();

            foreach (var marketPriceOnDay in MarketPriceDataSumByDay)
            {
                List<MarketPriceDTO> dailyMarketPrice = GetMarketPriceOfDay(marketPriceDataSet, marketPriceOnDay.Time);

                MarketPriceDay.Add(dailyMarketPrice);
            }
        }

        private void UpdateMostCheapTime(List<MarketPrice> marketPriceDataSet)
        {
            MarketPrice cheapestMarketPrice = marketPriceDataSet.Aggregate((time1, time2) => time1.Price < time2.Price ? time1 : time2);
            MostCheapTime = new MarketPriceDTO(cheapestMarketPrice);
        }

        private void UpdateMostExpensiveTime(List<MarketPrice> marketPriceDataSet)
        {
            MarketPrice mostExpensiveMarketPrice = marketPriceDataSet.Aggregate((time1, time2) => time1.Price > time2.Price ? time1 : time2);
            MostExpensiveTime = new MarketPriceDTO(mostExpensiveMarketPrice);
        }

        private void UpdateAverageCost(List<MarketPrice> marketPriceDataSet)
        {
            AverageCost = marketPriceDataSet.Average((time) => time.Price);
        }

        private void UpdateMostExpensiveHourWindow(List<MarketPrice> marketPriceData)
        {
            MostExpensiveHourWindow = new HourWindow
            {
                StartWindow = new MarketPriceDTO(marketPriceData.ElementAt(0)),
                EndWindow = new MarketPriceDTO(marketPriceData.ElementAt(1))
            };
            MostExpensiveHourWindow.Cost = MostExpensiveHourWindow.StartWindow.Price + MostExpensiveHourWindow.EndWindow.Price;

            for (int i = 1; i < marketPriceData.Count() - 1; i++)
            {
                MarketPriceDTO actualStartWindow = new MarketPriceDTO(marketPriceData.ElementAt(i));
                MarketPriceDTO actualEndWindow = new MarketPriceDTO(marketPriceData.ElementAt(i + 1));
                decimal windowCost = actualStartWindow.Price + actualEndWindow.Price;

                if (windowCost > MostExpensiveHourWindow.Cost)
                {
                    MostExpensiveHourWindow.StartWindow = actualStartWindow;
                    MostExpensiveHourWindow.EndWindow = actualEndWindow;
                    MostExpensiveHourWindow.Cost = windowCost;
                }
            }
        }
    }
}