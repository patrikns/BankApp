using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(Uppgift2BankApp.Areas.Identity.IdentityHostingStartup))]
namespace Uppgift2BankApp.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}