using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ZooWorld
{
    public class GameLifeTimeScope : LifetimeScope
    {
        public FoodChainRulesConfig FoodChainRulesConfig;
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<AnimalFactory>(Lifetime.Singleton);
            
            builder.RegisterInstance(FoodChainRulesConfig);
            builder.Register<FoodChainService>(Lifetime.Singleton);

            builder.RegisterEntryPoint<GameEntryPoint>();
        }
    }
}