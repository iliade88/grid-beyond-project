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
        public List<MarketPriceDTO> MarketPriceData { get; set; }

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
            UpdateMarketPriceData(marketPriceDataSet);
            UpdateMostCheapTime();
            UpdateMostExpensiveTime();
            UpdateAverageCost();
            UpdateMostExpensiveHourWindow();
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

        private void UpdateAverageCost()
        {
            AverageCost = MarketPriceData.Average((time) => time.Price);
        }

        private void UpdateMostExpensiveHourWindow()
        {
            MostExpensiveHourWindow = new HourWindow
            {
                StartWindow = MarketPriceData.ElementAt(0),
                EndWindow = MarketPriceData.ElementAt(1)
            };
            MostExpensiveHourWindow.Cost = MostExpensiveHourWindow.StartWindow.Price + MostExpensiveHourWindow.EndWindow.Price;

            for (int i = 1; i < MarketPriceData.Count() - 1; i++)
            {
                MarketPriceDTO actualStartWindow = MarketPriceData.ElementAt(i);
                MarketPriceDTO actualEndWindow = MarketPriceData.ElementAt(i + 1);
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