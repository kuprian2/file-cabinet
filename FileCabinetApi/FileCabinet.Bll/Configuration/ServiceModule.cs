using Autofac;
using FileCabinet.Bll.Contracts.Services;
using FileCabinet.Bll.Services;
using FileCabinet.Bll.StorageServices;
using FileCabinet.Dal.Configuration;

namespace FileCabinet.Bll.Configuration
{
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule<RepositoryModule>();

            builder.RegisterType<TagService>().As<ITagService>();
            builder.RegisterType<UserService>().As<IUserService>();
            builder.RegisterType<FileService>().As<IFileService>();

            builder.RegisterType<LocalStorageService>().As<IFileStorageService>();

            base.Load(builder);
        }
    }
}