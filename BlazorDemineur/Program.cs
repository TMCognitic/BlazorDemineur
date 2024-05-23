using BlazorDemineur.Messaging;
using BlazorDemineur.Messaging.Messages;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;

namespace BlazorDemineur
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddSingleton<IMediator<NewBombMessage>>(sp => Mediator<NewBombMessage>.Instance);
            builder.Services.AddSingleton<IMediator<NewDisplayedCaseMessage>>(sp => Mediator<NewDisplayedCaseMessage>.Instance);

            await builder.Build().RunAsync();
        }
    }
}
