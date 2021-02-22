using UnityEngine;
using UnityEngine.UI;
using TMPro;
using ProjectTime.Buildings;
using ProjectTime.Core;
using ProjectTime.Resources;
using ProjectTime.Power;

namespace ProjectTime.UI
{
    public class PowerGeneratorBuildingUI : BuildingUI
    {
        PowerGenerator targetBuilding;

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
            targetBuilding = (PowerGenerator)target.GetComponent<Building>();
        }

        private void OnGUI()
        {
            DrawGUI(targetBuilding);
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