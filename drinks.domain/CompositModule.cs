namespace drinks.domain
{
    using drinks.domain.@interface.services;
    using drinks.domain.services;
    using Ninject.Modules;

    public class CompositeModule : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind<IDrinkService>().To<DrinkService>().InSingletonScope();
        }
    }
}
