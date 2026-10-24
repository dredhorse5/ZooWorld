using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ZooWorld
{
    public class GameLifeTimeScope : LifetimeScope
    {
        public FoodChainRulesConfig FoodChainRulesConfig;
        public TastyFeedbackConfig TastyFeedbackConfig;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<AnimalFactory>(Lifetime.Singleton);

            builder.RegisterInstance(FoodChainRulesConfig);

            builder.RegisterInstance(TastyFeedbackConfig);

            builder.Register<AnimalEvents>(Lifetime.Singleton).As<IAnimalEvents>();
            builder.Register<AnimalDeathService>(Lifetime.Singleton).As<IAnimalDeathService>();
            builder.Register<ZooStatistics>(Lifetime.Singleton).As<IZooStatistics>();
            builder.Register<TastyFeedbackService>(Lifetime.Singleton).As<ITastyFeedbackService>();
            builder.Register<FoodChainService>(Lifetime.Singleton);

            builder.RegisterEntryPoint<GameEntryPoint>();
        }
    }
}
