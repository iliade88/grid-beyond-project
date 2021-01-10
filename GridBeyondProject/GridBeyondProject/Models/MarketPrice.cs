using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GridBeyondProject.Models
{
        [Table("MarketPrices")]
        public class MarketPrice
        {
            [Key]
            public int Id { get; set; }
            public DateTime Time { get; set; }
            public decimal Price { get; set; }

            public MarketPrice() { }
        }

        public class MarketPriceDBContext : DbContext
        {
            public DbSet<MarketPrice> MarketPrices { get; set; }
        }
}