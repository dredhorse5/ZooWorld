using UnityEngine;
using VContainer;
using TMPro;

namespace ZooWorld
{
    public class ZooHudView : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI deadPreysText;

        [SerializeField]
        private TextMeshProUGUI deadPredatorsText;

        [Inject]
        private IZooStatistics statistics;

        private bool subscribed;

        private void Start()
        {
            if (statistics == null)
                return;

            statistics.Changed += Refresh;
            subscribed = true;
            Refresh();
        }

        private void OnDestroy()
        {
            if (subscribed)
                statistics.Changed -= Refresh;
        }

        private void Refresh()
        {
            if (deadPreysText != null)
                deadPreysText.text = $"Dead preys: {statistics.DeadPreys}";

            if (deadPredatorsText != null)
                deadPredatorsText.text = $"Dead predators: {statistics.DeadPredators}";
        }
    }
}
