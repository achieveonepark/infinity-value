using Achieve.InfinityValue;
using UnityEngine;

namespace Achieve.InfinityValue.Samples
{
    public sealed class BasicUsageExample : MonoBehaviour
    {
        [SerializeField] private long startGold = 1_500_000L;
        [SerializeField] private long rewardGold = 12_500L;

        private InfinityValue _gold;

        private void Start()
        {
            _gold = new InfinityValue(startGold);
            Debug.Log($"Start gold: {_gold}");

            AddReward();
        }

        [ContextMenu("Add Reward")]
        public void AddReward()
        {
            _gold += rewardGold;
            Debug.Log($"Gold after reward: {_gold}");

            if (_gold > 1_000_000L)
                Debug.Log("Gold is above one million.");
        }
    }
}
