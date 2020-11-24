using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace TeamCityLightbox
{
    class Program
    {
        static void Main(string[] args)
        {

            var stepName = args[0];
            var buildId = Convert.ToInt32(args[1]);

            var build = GetBuildStatus(buildId);

            if(stepName == "Led_ON")
            {
                Console.WriteLine("slide");
                SerialWrite("slide");
            }

            if (stepName == "Led_OFF")
            {
                var cmd = build.Status == "SUCCESS" ? "on" : "blink";
                Console.WriteLine(cmd);
                SerialWrite(cmd);

                Thread.Sleep(Convert.ToInt32(ConfigurationManager.AppSettings["delay"]));
                Console.WriteLine("off");
                SerialWrite("off");
            }

        }


        private static void SerialWrite(string cmd)
        {
            var serialPort = new SerialPort(ConfigurationManager.AppSettings["serialPort"], 9600, Parity.None, 8, StopBits.One);
            serialPort.Open();

            serialPort.WriteLine(cmd);

            serialPort.Close();
        }

        private static dynamic GetBuildStatus(int buildId)
        {
            var baseUrl = ConfigurationManager.AppSettings["teamCity_url"];
            var accessToken = ConfigurationManager.AppSettings["teamCity_accessToken"];

            var client = new RestClient($"{baseUrl}/app/rest/builds/{buildId}");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", $"Bearer {accessToken}");
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);

            return Json.Decode(response.Content);
        }

    }
}
