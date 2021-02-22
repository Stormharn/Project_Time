using UnityEngine;
using UnityEngine.UI;
using TMPro;
using ProjectTime.Buildings;
using ProjectTime.Core;
using ProjectTime.Shielding;
using ProjectTime.Resources;

namespace ProjectTime.UI
{
    public class ShieldGeneratorBuildingUI : BuildingUI
    {
        [SerializeField] Button expandShieldButton;
        [SerializeField] Button shrinkShieldButton;

        ShieldGenerator targetBuilding;

        private void Start()
        {
            Setup();
            expandShieldButton.onClick.AddListener(ExpandShields);
            shrinkShieldButton.onClick.AddListener(ShrinkShields);
        }

        public override void DeleteBuilding()
        {
            ResourceManager.Instance.Refund(targetBuilding.BuildCost, .5f);
            var curCitizens = targetBuilding.GetCitizens();
            targetBuilding.RemovePopulation(curCitizens.Count);
            targetBuilding.Remove(false);
        }


        private void ShrinkShields()
        {
            targetBuilding.ReduceShields();
        }

        private void ExpandShields()
        {
            targetBuilding.ExpandShields();
        }

        public override void SetTarget(GameObject target)
        {
            targetBuilding = (ShieldGenerator)target.GetComponent<Building>();
        }

        private void OnGUI()
        {
            DrawGUI(targetBuilding);
            if (targetBuilding.isOnCooldown())
            {
                expandShieldButton.interactable = false;
                shrinkShieldButton.interactable = false;
            }
            else
            {
                expandShieldButton.interactable = true;
                shrinkShieldButton.interactable = true;
            }
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