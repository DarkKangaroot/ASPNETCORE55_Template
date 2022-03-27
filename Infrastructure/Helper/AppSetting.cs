using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Helper
{
    public static class AppSettings
    {
        public static object GetValues(string key)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            var configuration = new ConfigurationBuilder()
                .AddJsonFile(path, false)
                .Build();

            return configuration[key];
        }
    }
}
