using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Polly;

namespace Acme
{
    public class ProductsFunction
    {
        readonly ILogger _logger;
        readonly IBackend _backend = new FakeBackend();
        readonly Policy _latencyPolicy = PolicyBuilder.CreateLatency();
        readonly Policy _faultPolicy = PolicyBuilder.CreateFault();

        public ProductsFunction(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger($"Acme.{nameof(ProductsFunction)}");
        }

        [FunctionName("ProductsFunction")]
        public Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req)
        {
            _logger.LogInformation($"{nameof(ProductsFunction)} trigger function processed a request.");

            var sku = string.IsNullOrEmpty(req.Query["sku"])
                ? (int?)null
                : Convert.ToInt32(req.Query["sku"]);

            var policyWrap = Policy.Wrap(_latencyPolicy, _faultPolicy);
                
            var outcome = policyWrap.ExecuteAndCapture(() => _backend.GetProducts(sku));
            switch (outcome.FinalException == null) {
                default:
                    return Task.FromResult<IActionResult>(new OkObjectResult(outcome.Result));
                case false:
                     _logger.LogException(outcome.FinalException);
                    return Task.FromResult<IActionResult>(new BadRequestObjectResult(outcome.FinalException));
            }
        }
    }
}