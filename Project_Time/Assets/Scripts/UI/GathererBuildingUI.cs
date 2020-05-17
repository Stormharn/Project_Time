using UnityEngine;
using UnityEngine.UI;
using TMPro;
using ProjectTime.Build;
using ProjectTime.Core;
using ProjectTime.Resources;

namespace ProjectTime.UI
{
    public class GathererBuildingUI : BuildingUI
    {
        [SerializeField] TextMeshProUGUI typeUI;
        [SerializeField] TextMeshProUGUI amountUI;

        GathererBuilding targetBuilding;

        private void Start()
        {
            Setup();
        }

        public override void DeleteBuilding()
        {
            ResourceManager.Instance.Refund(targetBuilding.BuildCost, .5f);
            var curCitizens = targetBuilding.GetCitizens();
            targetBuilding.RemovePopulation(curCitizens.Count);
            targetBuilding.Remove(false);
        }

        public override void SetTarget(GameObject target)
        {
            targetBuilding = (GathererBuilding)target.GetComponent<Building>();
        }

        private void OnGUI()
        {
            DrawGUI(targetBuilding);
            typeUI.text = targetBuilding.ResourceType.ToString();
            amountUI.text = targetBuilding.ResourceCapacity.ToString();
        }

        public override void ToggleWorking()
        {
            targetBuilding.ToggleWorking();
        }

        public override void AddPopulation()
        {
            targetBuilding.AddPopulation(1);
        }

        public override void RemovePopulation()
        {
            targetBuilding.RemovePopulation(1);
        }
    }
}