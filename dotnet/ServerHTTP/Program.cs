using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;

public class Program
{
    public static void Main(string[] args)
    {
        // Create and configure the web server
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.ConfigureKestrel(options =>
                    {
                        // Configuration to enable HTTP/3
                        options.ListenAnyIP(8080, listenOptions =>
                        {
                            listenOptions.UseHttps(); // Use HTTPS with an appropriate certificate
#pragma warning disable CA2252
                            listenOptions.Protocols = HttpProtocols.Http1AndHttp2AndHttp3;
#pragma warning restore CA2252
                        });
                    })
                    .Configure(app =>
                    {
                        // Configure the request pipeline
                        app.Run(async context =>
                        {
                            // Define a simple HTML response
                            string responseString = "<html><body><h1>Simple HTTP Server with HTTP/3</h1><p>Hello, world!</p></body></html>";
                            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
                        
                            // Set response headers
                            context.Response.ContentType = "text/html; charset=utf-8";
                            context.Response.ContentLength = buffer.Length;
                        
                            // Write the response body
                            await context.Response.Body.WriteAsync(buffer, 0, buffer.Length);
                        });
                    });
            });
}