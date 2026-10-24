using VContainer;
using VContainer.Unity;

namespace ZooWorld
{
    public class GameEntryPoint : IStartable
    {
        [Inject]
        public GameEntryPoint(IZooStatistics statistics, ITastyFeedbackService tastyFeedback)
        {
        }

        public void Start()
        {
        }
    }
}
