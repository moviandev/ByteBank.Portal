using ByteBank.Portal.infra;
using System;

namespace ByteBankPortal
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var prefixes = new string[] { "http://localhost:5341/" };
            var webApp = new WebApp(prefixes);
            webApp.Start();
        }
    }
}