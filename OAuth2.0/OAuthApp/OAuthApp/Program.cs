using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAuthApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string applicationId = ConfigurationManager.AppSettings["appId"];
            string clientsecret = ConfigurationManager.AppSettings["clientsecret"];
        }
    }
}
