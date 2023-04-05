using Idata.Data;

namespace Page.Data.Seeders
{
    public class PageSeeder
    {
        public static async void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<IdataContext>();

                context.Database.EnsureCreated();

                PageModuleSeeder.Seed(applicationBuilder);
                await CMSPageSeeder.Seed(applicationBuilder);

            }
        }
    }
}