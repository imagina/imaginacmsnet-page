using Core;
using Core.Transformers;
using Idata.Data;
using Idata.Data.Entities;
using Idata.Data.Entities.Page;
using Isite.Repositories;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace Page.Data.Seeders
{
    public class CMSPageSeeder
    {
        public static async Task Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var IdataContext = serviceScope.ServiceProvider.GetService<IdataContext>();

                IdataContext.Database.EnsureCreated();

                ModuleRepository moduleRepository = new ModuleRepository();
                //Getting the modules in db
                dynamic modules = await moduleRepository.GetItemsBy(new UrlRequestBase());

                //transforming modules
                modules = await TransformerBase.TransformCollection(modules);

                foreach (var module in modules)
                {
                    if (module["cmsPages"] != null)
                    {
                        foreach (var cmsPage in module["cmsPages"])
                        {

                            JObject xPage = JObject.FromObject(cmsPage);
                            var key = xPage.SelectToken("Key");
                            var value = xPage.SelectToken("Value");

                            foreach (var configPage in value)
                            {
                                JObject yPage = new JObject(configPage);
                                foreach (JProperty property in yPage.Properties())
                                {

                                    string systemName = $"{module["alias"]}_cms_{key}_{property.Name}";
                                    object values = new
                                    {
                                        template = "default",
                                        is_home = false,
                                        status = property.Value.SelectToken("activated"),
                                        type = "cms",
                                        system_name = systemName,
                                        options = property.Value.ToString(),
                                        title = ""
                                    };

                                    Idata.Data.Entities.Page.Page? page = IdataContext.Page.Where(p => p.system_name == systemName).FirstOrDefault();

                                    if (page == null)
                                    {
                                        page = JsonConvert.DeserializeObject<Idata.Data.Entities.Page.Page?>(JsonConvert.SerializeObject(values));
                                        IdataContext.Page.Add(page);
                                        IdataContext.SaveChanges();
                                        page = IdataContext.Page.Where(p => p.system_name == systemName).FirstOrDefault();
                                        page.translations = new List<PageTranslations>() {
                                            new PageTranslations()
                                                {
                                                    locale = "en",
                                                    title = property.Value.SelectToken("title")?.ToString()
                                                }
                                            };

                                    }
                                    else
                                    {
                                        IdataContext.Entry(page).CurrentValues.SetValues(values);
                                    }

                                    IdataContext.SaveChanges();
                                }



                            }

                        }

                    }
                }



            }
        }
    }
}