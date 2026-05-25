using Achieve.InfinityValue;
using UnityEngine;

namespace Achieve.InfinityValue.Samples
{
    public sealed class SaveLoadExample : MonoBehaviour
    {
        [SerializeField] private string prefsKey = "infinity-value.gold";
        [SerializeField] private long firstRunGold = 1_000L;
        [SerializeField] private long rewardGold = 250L;

        private InfinityValue _gold;

        private void Awake()
        {
            string raw = PlayerPrefs.GetString(prefsKey, "0");
            if (!InfinityValue.TryParse(raw, out _gold) || _gold.IsEmpty)
                _gold = new InfinityValue(firstRunGold);

            Debug.Log($"Loaded gold: {_gold}");
        }

        [ContextMenu("Earn And Save")]
        public void EarnAndSave()
        {
            _gold += rewardGold;
            PlayerPrefs.SetString(prefsKey, _gold.ToString());
            PlayerPrefs.Save();

            Debug.Log($"Saved gold: {_gold}");
        }

        [ContextMenu("Clear Save")]
        public void ClearSave()
        {
            PlayerPrefs.DeleteKey(prefsKey);
            _gold = new InfinityValue(firstRunGold);

            Debug.Log($"Reset gold: {_gold}");
        }
    }
}
