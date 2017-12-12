using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Nethereum.Web3;

namespace AzureFunction.SampleNet
{
    public static class GetBalance
    {
        [FunctionName("GetBalanceFunction")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "GetBalance/address/{address}")]HttpRequestMessage req, string address, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");

            var web3 = new Nethereum.Web3.Web3("https://mainnet.infura.io");
            var balance = await web3.Eth.GetBalance.SendRequestAsync(address);

            // Fetching the name from the path parameter in the request URL
            return req.CreateResponse(HttpStatusCode.OK, "Balance of:" + address + ", Total: " + Web3.Convert.FromWei(balance.Value));
        }
    }
}
