namespace drinks.infrastructure
{
    using Ninject.Modules;

    public class CompositeModule : NinjectModule
    {
        public override void Load()
        {
            //Kernel.Bind<IFileService>().To<FileService>().InSingletonScope();
        }
    }
}
