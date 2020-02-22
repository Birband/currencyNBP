using System;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Http;

namespace currencyApi
{
    class Program
    {

        public class RootObject
        {
            public string table { get; set; }
            public string currency { get; set; }
            public string code { get; set; }
            public List<Rate> rates { get; set; }
        }

        public class Rate
        {
            public string no { get; set; }
            public string effectiveDate { get; set; }
            public double bid { get; set; }
            public double ask { get; set; }
        }

        static void Main(string[] args)
        {
            string text = Console.ReadLine();
            Console.Clear();

            //Getting data from the NBP api
            Uri url = new Uri("http://api.nbp.pl/api/exchangerates/rates/c/" + text + "?format=json");
            HttpClient client = new HttpClient();
            var response = client.GetStringAsync(url).Result;

            //Converting from the JSON data to regular c# objects
            RootObject result = JToken.Parse(response).ToObject<RootObject>();

            //The result
            Console.WriteLine("Currency: " + result.currency);
            Console.WriteLine("From date: " + result.rates[0].effectiveDate);
            Console.WriteLine("Bid: " + result.rates[0].bid);
            Console.WriteLine("Ask: " + result.rates[0].ask);

            Console.ReadLine();
        }


    }
}