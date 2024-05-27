namespace OrderApp
{
    using Newtonsoft;
    using Newtonsoft.Json;
    using System.Text;

    internal class Program
    {
        public class OrderStatus
        {
            public string orderId { get; set; }
            public string orderStatus { get; set; }

        }
        public class Status
        {
            public string errorCode { get; set; }
            public string httpStatus { get; set; }
            public string internalErrorCode { get; set; }
            public string internalErrorMessage { get; set; }
        }
        public class Order
        {
            public string dhanClientId { get; set; }
            public string correlationId { get; set; }
            public string transactionType { get; set; }
            public string exchangeSegment { get; set; }
            public string productType { get; set; }
            public string orderType { get; set; }
            public string validity { get; set; }
            public string tradingSymbol { get; set; }
            public string securityId { get; set; }
            public int quantity { get; set; }
            public int disclosedQuantity { get; set; }
            public double price { get; set; }
            public double triggerPrice { get; set; }
            public bool afterMarketOrder { get; set; }
            public string amoTime { get; set; }
            public double boProfitValue { get; set; }
            public double boStopLossValue { get; set; }
            public string drvExpiryDate { get; set; }
            public string drvOptionType { get; set; }
            public double drvStrikePrice { get; set; }
        }

        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            await PlaceOrder();
            Console.ReadKey();
        }

        static async Task PlaceOrder()
        {
            Order ord = new Order
            {
                afterMarketOrder = false,
                amoTime = "12:00:00",
                boProfitValue = 1.001,
                boStopLossValue = 1.00,
                correlationId = "101",
                dhanClientId = "cid1001",
                disclosedQuantity = 100,
                drvExpiryDate = "01/01/2025",
                drvOptionType = "CALL",
                drvStrikePrice = 1.001,
                orderType = "ABC",
                transactionType = "BUY"
            };

            var jsonOrder = JsonConvert.SerializeObject(ord);
            var httpContent = new StringContent(jsonOrder, Encoding.UTF8, "application/json");

            string url = "https://api.dhan.co/orders";
            using (var httpClient = new HttpClient())
            {
                var httpResponse = await httpClient.PostAsync(url, httpContent);
                if (httpResponse.IsSuccessStatusCode)
                {
                    // success
                    if (httpResponse.Content != null)
                    {
                        var responseContent = await httpResponse.Content.ReadAsStringAsync();
                        var status = JsonConvert.DeserializeObject<OrderStatus>(responseContent);
                        //Validate the status.
                    }
                }
                else
                {
                    //failure
                    if (httpResponse.Content != null)
                    {
                        var responseContent = await httpResponse.Content.ReadAsStringAsync();
                        var status = JsonConvert.DeserializeObject<Status>(responseContent);
                        //Validate the status and do appropriate action.
                    }
                }
            }
        }
    }
}
