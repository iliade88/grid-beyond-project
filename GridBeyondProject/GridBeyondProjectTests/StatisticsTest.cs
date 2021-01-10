using GridBeyondProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

        [Fact]
        public void FindMaximumCostWhenIsTheFirstElement()
        {
            decimal[] costs = { 7, 4, 2 };
            List<MarketPrice> marketPriceData = InitializeTestDataSet(costs);

            Statistics statistics = new Statistics(marketPriceData);

            Assert.Equal(7, statistics.MostExpensiveTime.Price);
            Assert.Equal(GetStartDate(), statistics.MostExpensiveTime.Time);
        }

        [Fact]
        public void FindMaximumCostWhenIsTheLastElement()
        {
            decimal[] costs = { 2, 4, 7 };
            List<MarketPrice> marketPriceData = InitializeTestDataSet(costs);

            Statistics statistics = new Statistics(marketPriceData);

            Assert.Equal(7, statistics.MostExpensiveTime.Price);
            Assert.Equal(GetStartDate().AddMinutes(60), statistics.MostExpensiveTime.Time);
        }

        [Fact]
        public void FindMaximumCost()
        {
            decimal[] costs = { 4, 7, 2 };
            List<MarketPrice> marketPriceData = InitializeTestDataSet(costs);

            Statistics statistics = new Statistics(marketPriceData);

            Assert.Equal(7, statistics.MostExpensiveTime.Price);
            Assert.Equal(GetStartDate().AddMinutes(30), statistics.MostExpensiveTime.Time);
        }

        [Fact]
        public void CalculateAverage()
        {
            decimal[] costs = { 2, 4, 7 };
            List<MarketPrice> marketPriceData = InitializeTestDataSet(costs);
            decimal average = costs.Sum() / costs.Count();

            Statistics statistics = new Statistics(marketPriceData);

            Assert.Equal(average, statistics.AverageCost);
        }

        [Fact]
        public void FindMostExpensiveHourWindowWhenIsTheFirstElement()
        {
            decimal[] costs = { 7, 4, 2, 1 };
            List<MarketPrice> marketPriceData = InitializeTestDataSet(costs);

            Statistics statistics = new Statistics(marketPriceData);

            Assert.Equal(GetStartDate(), statistics.MostExpensiveHourWindow.StartWindow.Time);
            Assert.Equal(7, statistics.MostExpensiveHourWindow.StartWindow.Price);
            Assert.Equal(GetStartDate().AddMinutes(30), statistics.MostExpensiveHourWindow.EndWindow.Time);
            Assert.Equal(4, statistics.MostExpensiveHourWindow.EndWindow.Price);
            Assert.Equal(11, statistics.MostExpensiveHourWindow.Cost);
        }

        [Fact]
        public void FindMostExpensiveHourWindowWhenIsTheLastElement()
        {
            decimal[] costs = { 1, 2, 7, 4 };
            List<MarketPrice> marketPriceData = InitializeTestDataSet(costs);

            Statistics statistics = new Statistics(marketPriceData);

            Assert.Equal(GetStartDate().AddMinutes(60), statistics.MostExpensiveHourWindow.StartWindow.Time);
            Assert.Equal(7, statistics.MostExpensiveHourWindow.StartWindow.Price);
            Assert.Equal(GetStartDate().AddMinutes(90), statistics.MostExpensiveHourWindow.EndWindow.Time);
            Assert.Equal(4, statistics.MostExpensiveHourWindow.EndWindow.Price);
            Assert.Equal(11, statistics.MostExpensiveHourWindow.Cost);
        }

        [Fact]
        public void FindMostExpensiveHourWindow()
        {
            decimal[] costs = { 1, 7, 4, 2 };
            List<MarketPrice> marketPriceData = InitializeTestDataSet(costs);

            Statistics statistics = new Statistics(marketPriceData);

            Assert.Equal(GetStartDate().AddMinutes(30), statistics.MostExpensiveHourWindow.StartWindow.Time);
            Assert.Equal(7, statistics.MostExpensiveHourWindow.StartWindow.Price);
            Assert.Equal(GetStartDate().AddMinutes(60), statistics.MostExpensiveHourWindow.EndWindow.Time);
            Assert.Equal(4, statistics.MostExpensiveHourWindow.EndWindow.Price);
            Assert.Equal(11, statistics.MostExpensiveHourWindow.Cost);
        }
    }
}
