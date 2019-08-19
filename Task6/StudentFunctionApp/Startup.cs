using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

[assembly: FunctionsStartup(typeof(StudentFunctionApp.Startup))]

namespace StudentFunctionApp
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            //builder.Services.AddHttpClient();
            /*builder.Services.AddSingleton((s) => {
                return new CosmosClient(Environment.GetEnvironmentVariable("COSMOSDB_CONNECTIONSTRING"));
            });*/
            //builder.Services.AddSingleton<ILoggerProvider, MyLoggerProvider>();
            //builder.AddAzureStorage();
            //builder.Services.AddAzureStorage();

        }
    }
}
