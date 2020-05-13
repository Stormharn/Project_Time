using ProjectTime.Build;
using ProjectTime.Core;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace ProjectTime.UI
{
    public abstract class BuildingUI : MonoBehaviour
    {
        [SerializeField] internal Button closeButton;
        [SerializeField] internal Button deleteBuildingButton;
        [SerializeField] internal TextMeshProUGUI powerText;
        [SerializeField] internal Button togglePowerButton;

        internal Player player;

        public abstract void SetTarget(GameObject target);

        public abstract void DeleteBuilding();
    }
}