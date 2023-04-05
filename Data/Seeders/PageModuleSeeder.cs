using Idata.Data;
using Idata.Data.Entities;
using Idata.Data.Entities.Isite;
using Isite.Data;
using Newtonsoft.Json;

namespace Page.Data.Seeders
{
    public class PageModuleSeeder
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<IdataContext>();

                context.Database.EnsureCreated();

                object values = new
                {
                    name = "Page",
                    alias = "page",
                    permissions = "{'page.pages':{'index':'page::pages.list resource','index-all':'page::pages.list-all resource','create':'page::pages.create resource','edit':'page::pages.edit resource','destroy':'page::pages.destroy resource','manage':'page::pages.manage resource'}}",
                    enabled = true,
                    priority = 1,
                    cms_pages = "{'admin':{'pages':{'permission':'page.pages.manage','activated':true,'authenticated':true,'path':'/page/pages/index','name':'qpage.admin.pages','crud':'qpage/_crud/pages','page':'qcrud/_pages/admin/crudPage','layout':'qsite/_layouts/master.vue','title':'page.cms.sidebar.adminPages','icon':'fas fa-columns','subHeader':{'refresh':true}}},'panel':[],'main':[]}",
                    cms_sidebar = "{'admin':['page_cms_admin_pages'],'panel':[]}"
                };


                Module? module = context.Modules.Where(m => m.alias == "page").FirstOrDefault();

                if (module == null)
                {
                    module = JsonConvert.DeserializeObject<Module>(JsonConvert.SerializeObject(values));
                    context.Modules.Add(module);
                    context.SaveChanges();
                    module = context.Modules.Where(m => m.alias == "page").FirstOrDefault();
                    module.translations = new List<ModuleTranslation>() {
                        new ModuleTranslation()
                            {
                                locale = "en",
                                title = "Page"
                            }
                        };

                }
                else
                {
                    context.Entry(module).CurrentValues.SetValues(values);
                }



                context.SaveChanges();

            }
        }
    }
}
