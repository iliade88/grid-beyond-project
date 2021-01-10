using GridBeyondProject.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace GridBeyondProject.Services
{
    public class UploadService
    {
        private static Microsoft.VisualBasic.FileIO.TextFieldParser GetCSVParser(string filePath)
        {
            var parser = new Microsoft.VisualBasic.FileIO.TextFieldParser(filePath);
            parser.TextFieldType = Microsoft.VisualBasic.FileIO.FieldType.Delimited;
            parser.SetDelimiters(new string[] { "," });

            return parser;
        }

        private static List<MarketPrice> GetMarketPricesFromCSV(Microsoft.VisualBasic.FileIO.TextFieldParser parser)
        {
            List<MarketPrice> marketPrices = new List<MarketPrice>();

            //Discard headers
            string[] headers = parser.ReadFields();

            while (!parser.EndOfData)
            {
                string[] row = parser.ReadFields();
                marketPrices.Add(new MarketPrice { Time = DateTime.Parse(row[0]), Price = decimal.Parse(row[1]) });
            }

            return marketPrices;
        }

        private static void SaveMarketPricesInDB(List<MarketPrice> newMarketPrices)
        {
            MarketPriceDBContext db = new MarketPriceDBContext();
            db.MarketPrices.AddRange(newMarketPrices);
            db.SaveChanges();
        }

        public static void ProcessFile(string newFileName)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "UploadedFiles", Path.GetFileName(newFileName));

            var parser = GetCSVParser(path); 

            List<MarketPrice> marketPrices = GetMarketPricesFromCSV(parser);

            SaveMarketPricesInDB(marketPrices);
        }
    }
}