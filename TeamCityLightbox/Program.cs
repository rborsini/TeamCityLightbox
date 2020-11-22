using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TeamCityLightbox
{
    class Program
    {
        static void Main(string[] args)
        {
            var serialPort = new SerialPort(ConfigurationManager.AppSettings["serialPort"], 9600, Parity.None, 8, StopBits.One);

            serialPort.Open();

            foreach(var arg in args)
            {
                var parts = arg.Split(':');
                var cmd = parts[0];
                var delay = parts.Length == 2 ? Convert.ToInt32(parts[1]) : 0;

                serialPort.WriteLine(cmd);
                Thread.Sleep(delay);
            }

            //serialPort.WriteLine("slide");
            //Thread.Sleep(5000);
            //serialPort.WriteLine("blink");
            //Thread.Sleep(5000);
            //serialPort.WriteLine("off");

            serialPort.Close();

        }
    }
}
