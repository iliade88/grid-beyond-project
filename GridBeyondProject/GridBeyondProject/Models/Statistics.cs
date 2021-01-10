using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GridBeyondProject.Models
{
    public class Statistics
    {
        public List<MarketPriceDTO> MarketPriceData { get; set; }

        public Statistics(List<MarketPrice> marketPriceDataSet)
        { 
            MarketPriceData = marketPriceDataSet
                    .Select(x => new MarketPriceDTO { Time = x.Time, Price = x.Price })
                    .ToList();
        }
    }
}