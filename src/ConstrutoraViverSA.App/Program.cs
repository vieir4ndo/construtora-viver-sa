using ConstrutoraViverSA.Infraestrutura;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace ConstrutoraViverSA.App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using var db = new ApplicationContext();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
