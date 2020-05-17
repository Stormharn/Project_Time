using UnityEngine;
using TMPro;
using ProjectTime.Resources;

namespace ProjectTime.UI
{
    public class ResourceText : MonoBehaviour
    {
        [SerializeField] ResourceTypes types;
        TextMeshProUGUI textMeshPro;

        private void Awake()
        {
            textMeshPro = GetComponent<TextMeshProUGUI>();
        }

        public void OnGUI()
        {
            (float current, float max) result = (0, 0);
            switch (types)
            {
                case ResourceTypes.Wood:
                    result = ResourceManager.Instance.GetWoodStats();
                    break;
                case ResourceTypes.Stone:
                    result = ResourceManager.Instance.GetStoneStats();
                    break;
                case ResourceTypes.Steel:
                    result = ResourceManager.Instance.GetSteelStats();
                    break;
                case ResourceTypes.Food:
                    result = ResourceManager.Instance.GetFoodStats();
                    break;
            }

            textMeshPro.text = string.Format("{0:0} / {1:0}", result.current.ToString(), result.max.ToString());
        }
    }
}