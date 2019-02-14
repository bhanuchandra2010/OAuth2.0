using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OAuthApp
{
    class Program
    {
        private static string clientSecret;

        static void Main(string[] args)
        {
            string url = ConfigurationManager.AppSettings["Url"];

            if (string.IsNullOrEmpty(url))
            {
                Console.WriteLine("Please fill in your URL:");
                url = Console.ReadLine();
            }

            Console.WriteLine("Calling url: " + url);

            TestAPI(url);
            Console.WriteLine("Done processing, press any key to close....");
            Console.ReadKey();
        }

        private static void TestAPI(string url)
        {
            var authresult = GetToken();
            if (authresult.AccessToken != null)
            {
                

                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authresult.AccessToken);
                var response = client.GetAsync(new Uri(url));
                string content = response.Result.Content.ReadAsStringAsync().Result;
                 Console.WriteLine(content);
            }
        }

        private static AuthenticationResult GetToken()
        {
            
            string addInstance = "https://login.microsoftonline.com/220089ff-1b38-434b-a3e7-700d2af82066/oauth2/v2.0/token";
            string resourceId = ConfigurationManager.AppSettings["resourceid"];
            string tenantid = ConfigurationManager.AppSettings["tenantid"];
            string clientId = ConfigurationManager.AppSettings["clientId"];
            string clientsecret = "_/#*#.|.*0!;(R:}:.(|]5;#dh^!&x+/(T2)z-!#*$PYB@=-{D|_$7-f.&V";
            string replyaddress = ConfigurationManager.AppSettings["replyaddress"];
            AuthenticationContext context = new AuthenticationContext(string.Format(addInstance, tenantid));
            //AuthenticationResult result = context.AcquireToken(resourceId, new ClientCredential(clientId, clientsecret));
            AuthenticationResult result = context.AcquireTokenAsync(resourceId, clientId, new Uri("https://www.google.com"), PromptBehavior.Auto, new UserIdentifier("me", UserIdentifierType.OptionalDisplayableId), new PlatformParameters(PromptBehavior.Auto));
            //AuthenticationResult result = context.AcquireToken(resourceId,clientId,new Uri("https://www.google.com"),PromptBehavior.Auto,new UserIdentifier("me",UserIdentifierType.OptionalDisplayableId), "client_secret=_/#*#.|.*0!;(R:}:.(|]5;#dh^!&x+/(T2)z-!#*$PYB@=-{D|_$7-f.&V");
            context.TokenCache.Clear();
            return result;
        }
    }
}
