using ProjectTime.Resources;
using UnityEngine;
using TMPro;

namespace ProjectTime.UI
{
    public class ResourceText : MonoBehaviour
    {
        [SerializeField] ResourceTypes types;
        ResourceManager resourceManager;
        TextMeshProUGUI textMeshPro;

        private void Awake()
        {
            resourceManager = GameObject.FindObjectOfType<ResourceManager>();
            textMeshPro = GetComponent<TextMeshProUGUI>();
        }

        public void OnGUI()
        {
            (float current, float max) result = (0, 0);
            switch (types)
            {
                case ResourceTypes.Wood:
                    result = resourceManager.WoodStats();
                    break;
                case ResourceTypes.Stone:
                    result = resourceManager.StoneStats();
                    break;
                case ResourceTypes.Steel:
                    result = resourceManager.SteelStats();
                    break;
            }

            textMeshPro.text = string.Format("{0:0} / {1:0}", result.current.ToString(), result.max.ToString());
        }
    }
}