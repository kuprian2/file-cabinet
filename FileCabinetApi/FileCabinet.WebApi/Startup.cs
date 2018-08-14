using Autofac;
using Autofac.Integration.WebApi;
using FileCabinet.WebApi.Configuration;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;
using System.Reflection;
using System.Web.Http;

[assembly: OwinStartup(typeof(FileCabinet.WebApi.Startup))]

namespace FileCabinet.WebApi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var builder = new ContainerBuilder();

            builder.ResolveDependencies();
            builder.UseAutoMapper();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterWebApiModelBinderProvider();

            var container = builder.Build();

            var config = new HttpConfiguration();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            WebApiConfig.Register(config);
            app.UseCors(CorsOptions.AllowAll);

            app.UseAutofacMiddleware(container);
            ConfigureAuth(app);
            app.UseWebApi(config);
        }
    }
}
