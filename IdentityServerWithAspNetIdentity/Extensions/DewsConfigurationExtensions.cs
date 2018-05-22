using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServerWithAspNetIdentity.Extensions
{
    public static class DewsConfigurationExtensions
    {
        public static string GetDewsWebServerUrl(this IConfiguration configuration)
        {
            return configuration.GetValue<string>("DewsWebServerUri");
        }
    }
}
