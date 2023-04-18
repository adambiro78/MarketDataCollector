﻿using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace MarketDataCollector
{
    //a simple market data collector class
    public class DataCollectionService 
    {
        public readonly string _securitySymbol;
        public readonly string _filePath;

        public DataCollectionService(string SecuritySymbol, string FilePath) 
        {
            _securitySymbol = SecuritySymbol;
            _filePath = FilePath;
        }

        /*
         * only task is to collect a single point of BTC-PERPETUAL price and the corresponding timestamp 
         * and save to a .json object with an arbitrary name to the local directory
         */
        public async Task CollectMarketData()
        {
            var adapter = new DeribitExchangeAdapter();
            var marketdata = await adapter.GetMarketData(_securitySymbol);

        }
    }

    /*implements the methods for communication between the Deribit Exchange 
     * abstracting the exchange specific behaviors from the DataCollectionService
    */
    public class DeribitExchangeAdapter 
    {
        public readonly HttpClient _httpClient;
        public readonly string _baseUrl;
        public readonly string _clientId;
        public readonly string _clientSecret;

        public DeribitExchangeAdapter() 
        {
            _httpClient = new HttpClient();
            _baseUrl = "https://test.deribit.com/api/v2/"; 
            _clientId = "BNkS7vXd";
            _clientSecret = "SOzklmYh1dZrkaJGpWIgNDZ6nVnxmw6euk2T9I0DSaM";
        }

        public async Task<MarketData> GetMarketData(string securitySymbol) 
        {
            await Authorize();

            //TODO

            return new MarketData
            {
                AskPrice = 0,
                BidPrice = 0,
                SecuritySymbol = securitySymbol,
                Timestamp = DateTime.UtcNow.Ticks
            };
        }

        //https://docs.deribit.com/#public-auth
        private async Task Authorize()
        {
            /*
                curl -X GET 
                "https://test.deribit.com/api/v2/public/auth?
                    client_id=fo7WAPRm4P
                    &client_secret=W0H6FJW4IRPZ1MOQ8FP6KMC5RZDUUKXS
                    &grant_type=client_credentials" \
                -H "Content-Type: application/json"
             */
            var url = $"{_baseUrl}/public/auth";
            var requestBody = new
            {
                client_id = _clientId, 
                client_secret = _clientSecret,
                grant_type = "client_credentials"
            };

            //TODO
            HttpContent? requestBodyContent = null;

            //Send a POST request to the specified Uri
            var response = await _httpClient.PostAsync(url, requestBodyContent);
        }

    }

    /*
     .json object with an arbitrary name to the local directory with the structure based on the following example:
        {
        "SecuritySymbol": "BTC-PERPETUAL",
        "AskPrice": 41232.5,
        "BidPrice": 41232.8,
        "Timestamp": 1550230036440
        }
     */
    public class MarketData 
    {
        public string? SecuritySymbol;
        public decimal AskPrice;
        public decimal BidPrice;
        public long Timestamp;
    }

    //entry point
    class Program
    {
        //In the main method, an instance of DataCollectionService is created
        static async Task Main(string[] args)
        {
            var dataCollectionService = new DataCollectionService("BTC-PERPETUAL", "marketdata.json");
            await dataCollectionService.CollectMarketData();
        }
    }
}