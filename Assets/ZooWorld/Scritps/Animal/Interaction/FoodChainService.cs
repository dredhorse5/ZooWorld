using VContainer;

namespace ZooWorld
{
    public sealed class FoodChainService
    {
        private readonly FoodChainRulesConfig _config;
        private readonly IAnimalDeathService _deathService;

        [Inject]
        public FoodChainService(FoodChainRulesConfig config, IAnimalDeathService deathService)
        {
            _config = config;
            _deathService = deathService;
        }

        public void HandleCollision(Animal a, Animal b)
        {
            if (a == null || b == null || !a.IsAlive || !b.IsAlive)
                return;

            var rule = GetCollideRule(a.Type, b.Type);
            if (rule != Rules.NotImplemented)
            {
                ExecuteRule(a, b, rule);
                return;
            }

            rule = GetCollideRule(b.Type, a.Type);
            if (rule != Rules.NotImplemented)
                ExecuteRule(b, a, rule);
        }

        private void ExecuteRule(Animal a, Animal b, Rules rule)
        {
            switch (rule)
            {
                case Rules.Eat:
                    _deathService.Kill(b, a, wasEaten: true);
                    break;
                case Rules.Dead:
                    _deathService.Kill(b, a, wasEaten: false);
                    break;
            }
        }

        private Rules GetCollideRule(AnimalTypes a, AnimalTypes b)
        {
            if (_config.Rules.TryGetValue(a, out var d))
            {
                if (d.TryGetValue(b, out var rule))
                    return rule;
            }

            return Rules.NotImplemented;
        }
    }
}
