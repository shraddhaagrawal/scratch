using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace TwitterApp
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var twitter = new Twitter
            {
                OAuthConsumerKey = "G3fdEmo3a2rIHbzR70O7KQij0",
                OAuthConsumerSecret = "buFAGPjj4ingWBZmEMGYKnRsA0Y4jjQ5yZTXxQyn9obTHTnekq"
            };
            

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("<html><body style='background:url(https://github.com/shraddhaagrawal/scratch/blob/master/acs/TwitterApp/FlagBackground.jpg?raw=true)no-repeat;'>");
                IEnumerable<string> twittsClinton = twitter.GetTwitts("Hilary","Clinton",5).Result;
                IEnumerable<string> twittsTrump = twitter.GetTwitts("Donald", "trump", 5).Result;
                await context.Response.WriteAsync("<h1 style='color:blue;'>Recent 5 tweets mentioned Hillary clinton</h1>");
                foreach (var t in twittsClinton)
                {
                    await context.Response.WriteAsync(t + "<br>");
                }
                await context.Response.WriteAsync("<h1 style='color:blue;'>Recent 5 tweets mentioned Donald trump</h1>");
                foreach (var t in twittsTrump)
                {
                    await context.Response.WriteAsync(t + "<br>");
                }
                await context.Response.WriteAsync("</body></html>");
            });
        }
    }
}
