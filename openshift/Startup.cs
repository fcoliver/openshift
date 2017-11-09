using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System.Text;

namespace openshift
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync(Dir());
            });
        }

        public string Dir()
        {
            try
            {
                IFileProvider provider = new PhysicalFileProvider("/");
                IDirectoryContents contents = provider.GetDirectoryContents(""); // the applicationRoot contents
                StringBuilder ret = new StringBuilder();

                foreach (IFileInfo item in contents)
                {

                    ret.AppendLine(item.Name);

                }
                return ret.ToString();
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
       
           
        }
    }
}
