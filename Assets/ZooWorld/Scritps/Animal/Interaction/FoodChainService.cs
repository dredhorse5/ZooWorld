using VContainer;

namespace ZooWorld
{
    public sealed class FoodChainService
    {
        private readonly FoodChainRulesConfig _config;
        [Inject]
        public FoodChainService(FoodChainRulesConfig config)
        {
            _config = config;
        }

        public void HandleCollision(Animal a, Animal b)
        {
            if (a == null || b == null || !a.IsActive || !b.IsActive)
                return;

            var rule = GetCollideRule(a.Type, b.Type);
            if(rule != Rules.NotImplemented)
            {
                ExecuteRule(a, b, rule);
                return;
            }
            
            rule = GetCollideRule(b.Type, a.Type);
            if(rule != Rules.NotImplemented)
                ExecuteRule(b, a, rule);
        }


        private void ExecuteRule(Animal a, Animal b, Rules rule)
        {
            switch (rule)
            {
                case Rules.Eat:
                    a.TastyFeedback();
                    b.Death();
                    break;
                case Rules.Dead:
                    // "a" - no feedback
                    b.Death();
                    break;
            }
        }

        private Rules GetCollideRule(AnimalTypes a, AnimalTypes b)
        {
            if(_config.Rules.TryGetValue(a, out var d ))
            {
                if(d.TryGetValue(b, out var rule))
                    return rule;
            }

            return Rules.NotImplemented;
        }
    }
}