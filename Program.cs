using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using WebStore.Data;

namespace WebStore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var build = CreateHostBuilder(args).Build();
            AppDbContextSeed.Seed(build.Services);
            build.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
