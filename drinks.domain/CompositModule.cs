namespace drinks.domain
{
    using @interface.services;
    using services;
    using Ninject.Modules;

    public class CompositeModule : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind<IDrinkService>().To<DrinkService>().InSingletonScope();
            Kernel.Bind<ICoinService>().To<CoinService>().InSingletonScope();
        }
    }
}
