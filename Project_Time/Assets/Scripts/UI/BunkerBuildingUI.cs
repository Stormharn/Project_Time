using UnityEngine;
using UnityEngine.UI;
using TMPro;
using ProjectTime.Build;
using ProjectTime.Core;
using ProjectTime.Resources;

namespace ProjectTime.UI
{
    public class BunkerBuildingUI : BuildingUI
    {
        [SerializeField] Button expandShieldButton;
        [SerializeField] Button shrinkShieldButton;

        Bunker targetBuilding;

        private void Start()
        {
            Setup();
            expandShieldButton.onClick.AddListener(ExpandShields);
            shrinkShieldButton.onClick.AddListener(ShrinkShields);
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
            targetBuilding = (Bunker)target.GetComponent<Building>();
        }

        private void OnGUI()
        {
            if (targetBuilding.IsHomeBase)
                deleteBuildingButton.gameObject.SetActive(false);
            DrawGUI(targetBuilding);
            if (targetBuilding.ShieldGeneratorIsOnCooldown())
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

        public override void DeleteBuilding()
        {
            ResourceManager.Instance.Refund(targetBuilding.BuildCost, .5f);
            var curCitizens = targetBuilding.GetCitizens();
            targetBuilding.RemovePopulation(curCitizens.Count);
            targetBuilding.Remove(true);
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