using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace ServicesTests.Services
{
    internal static class ConnectionString
    {
        private static void SetDataDirectory()
        {
            string currentDir = Directory.GetCurrentDirectory();
            int IndexOfLastSlash = currentDir.LastIndexOf("bin\\");
            string projectDir = currentDir.Substring(0, IndexOfLastSlash);
            string path = Path.Combine(projectDir, "DB");
            AppDomain.CurrentDomain.SetData("DataDirectory", path);
        }

        public static string Get()
        {
            SetDataDirectory();

            var configurationManager = new ConfigurationManager()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json").Build();

            return configurationManager.GetConnectionString("DefaultConnection");
        }
    }
}
