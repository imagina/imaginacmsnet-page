using Idata.Data;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.EntityFrameworkCore;
using Page.Repositories;
using Page.Repositories.Interfaces;

namespace Page
{
    public static class PageServiceProvider
    {


        public static WebApplicationBuilder? Boot(WebApplicationBuilder? builder)
        {
            //TODO Implement controllerBase to avoid basic crud redundant code
            builder.Services.AddControllers().ConfigureApplicationPartManager(o =>
            {

                o.ApplicationParts.Add(new AssemblyPart(typeof(PageServiceProvider).Assembly));
            });

            //Repositories
            builder.Services.AddTransient<IPageRepository, PageRepository>();
            return builder;

        }
    }
}
