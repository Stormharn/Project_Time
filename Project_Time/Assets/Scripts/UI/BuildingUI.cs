using ProjectTime.Build;
using ProjectTime.Core;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectTime.UI
{
    public abstract class BuildingUI : MonoBehaviour
    {
        [SerializeField] internal Button closeButton;
        [SerializeField] internal Button deleteBuildingButton;

        internal Player player;

        public abstract void SetTarget(GameObject target);
    }
}