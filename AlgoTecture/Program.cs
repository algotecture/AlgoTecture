using System;
using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Web;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace AlgoTecture
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var logger = NLogBuilder.ConfigureNLog("NLog.config").GetCurrentClassLogger();
            LogManager.Configuration.Variables["fileNameDir"] = OperatingSystem.IsWindows() ? "\\AlgoTecture\\AlgoTectureMvp\\" : "/AlgoTecture/AlgoTectureMvp/";
            LogManager.Configuration.Variables["archiveFileNameDir"] = OperatingSystem.IsWindows() ? "\\AlgoTecture\\AlgoTectureMvp\\log\\" : "/AlgoTecture/AlgoTectureMvp/";
            
            try
            {
                CreateHostBuilder(args).Build().Run();
                logger.Debug("init main");
            }
            catch (Exception exception)
            {
                logger.Error(exception, "Stopped program because of exception");
                throw;
            }
            finally
            {
                LogManager.Shutdown();
            }
        }

        private static IWebHostBuilder CreateHostBuilder(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("hosting.json", true)
                .Build();
            var host = WebHost.CreateDefaultBuilder(args)
                .UseConfiguration(config)
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>()
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.SetMinimumLevel(LogLevel.Trace);
                })
                .UseNLog();

            return host;
        }
    }
}