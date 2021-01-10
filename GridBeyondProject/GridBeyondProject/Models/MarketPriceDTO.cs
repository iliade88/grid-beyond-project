using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GridBeyondProject.Models
{
    public class MarketPriceDTO
    {
        public DateTime Time { get; set; }
        public string ShortDateString { get; set; }
        public decimal Price { get; set; }

        public MarketPriceDTO() { }

        public MarketPriceDTO(MarketPrice marketPrice)
        {
            Time = marketPrice.Time;
            ShortDateString = marketPrice.Time.ToShortDateString();
            Price = marketPrice.Price;
        }
    }
}