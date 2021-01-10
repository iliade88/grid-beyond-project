using GridBeyondProject.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace GridBeyondProjectTests
{
    public class StatisticsTest
    {
        private DateTime GetStartDate()
        {
            return new DateTime(2017, 1, 1);
        }

        private List<MarketPrice> InitializeTestDataSet(decimal[] costs)
        {
            List<MarketPrice> dataset = new List<MarketPrice>();
            DateTime date = GetStartDate();

            for (int i = 0; i < costs.Length; i++)
            {
                dataset.Add(new MarketPrice { Time = date, Price = costs[i] });

                date = date.AddMinutes(30);
            }

            return dataset;
        }

        [Fact]
        public void FindMinimumCostWhenIsTheFirstElement()
        {
            decimal[] costs = { 2, 4, 7 };
            List<MarketPrice> marketPriceData = InitializeTestDataSet(costs);

            Statistics statistics = new Statistics(marketPriceData);

            Assert.Equal(2, statistics.MostCheapTime.Price);
            Assert.Equal(GetStartDate(), statistics.MostCheapTime.Time);
        }

        [Fact]
        public void FindMinimumCostWhenIsTheLastElement()
        {
            decimal[] costs = { 7, 4, 2 };
            List<MarketPrice> marketPriceData = InitializeTestDataSet(costs);

            Statistics statistics = new Statistics(marketPriceData);

            Assert.Equal(2, statistics.MostCheapTime.Price);
            Assert.Equal(GetStartDate().AddMinutes(60), statistics.MostCheapTime.Time);
        }

        [Fact]
        public void FindMinimumCost()
        {
            decimal[] costs = { 4, 2, 7 };
            List<MarketPrice> marketPriceData = InitializeTestDataSet(costs);

            Statistics statistics = new Statistics(marketPriceData);

            Assert.Equal(2, statistics.MostCheapTime.Price);
            Assert.Equal(GetStartDate().AddMinutes(30), statistics.MostCheapTime.Time);
        }
    }
}
