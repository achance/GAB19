using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Security.Cryptography;
using System.Net.Http.Headers;

namespace GABLogs
{
    class Program
    {
        static void Main(string[] args)
        {
            
            //Write something to log
            string result = SendLog(customerID, sharedKey, "TestLog1", @"[{""testProp"": ""Error""}]");

            Console.WriteLine(result);

            Console.ReadKey();
        }



        //update this value with the WorkSpace ID
        static string customerID = "331e6c3b-1d04-49fa-8b9b-699eb10cc110";

        //update this value with the Primary or Secondary Key from Connected Sources
        static string sharedKey = "YMxWrCWUeIuHu1Gg3g740CA0H6XUpa/TcHLY3Fi4ZlawBvtrZDU9ivkqJEQ8HG//r5MVJMfd9Yk95tFbFUePLA==";


        //Send log to Azure

        //Sample code pulled from https://docs.microsoft.com/en-us/azure/log-analytics/log-analytics-data-collector-api



        // Build the API signature

        public static string BuildSignature(string message, string secret)

        {

            var encoding = new System.Text.ASCIIEncoding();

            byte[] keyByte = Convert.FromBase64String(secret);

            byte[] messageBytes = encoding.GetBytes(message);

            using (var hmacsha256 = new HMACSHA256(keyByte))

            {

                byte[] hash = hmacsha256.ComputeHash(messageBytes);

                return Convert.ToBase64String(hash);

            }

        }


        public static string SendLog(string customerId, string sharedKey, string logName, string json, string timeStamp = "") //timeStamp will auto generate if not passed in

        {

            string result = "";



            try

            {

                // Create a hash for the API signature

                var dateString = DateTime.UtcNow.ToString("r");

                var jsonBytes = Encoding.UTF8.GetBytes(json);

                string stringToHash = "POST\n" + jsonBytes.Length + "\napplication/json\n" + "x-ms-date:" + dateString + "\n/api/logs";

                string hashedString = BuildSignature(stringToHash, sharedKey);

                string signature = "SharedKey " + customerId + ":" + hashedString;



                string url = "https://" + customerId + ".ods.opinsights.azure.com/api/logs?api-version=2016-04-01";



                System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();

                client.DefaultRequestHeaders.Add("Accept", "application/json");

                client.DefaultRequestHeaders.Add("Log-Type", logName);

                client.DefaultRequestHeaders.Add("Authorization", signature);

                client.DefaultRequestHeaders.Add("x-ms-date", dateString);

                client.DefaultRequestHeaders.Add("time-generated-field", timeStamp);



                System.Net.Http.HttpContent httpContent = new StringContent(json, Encoding.UTF8);

                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                Task<System.Net.Http.HttpResponseMessage> response = client.PostAsync(new Uri(url), httpContent);



                if (response.Result.IsSuccessStatusCode)

                {

                    result = $"{response.Result.StatusCode}"; //Returns OK

                }

                else

                {

                    result = $"API Post Exception: {response.Result.ToString()}";

                }







            }

            catch (Exception excep)

            {

                result = $"API Post Exception: {excep}";

            }



            return result;

        }

    }
}
