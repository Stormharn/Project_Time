using UnityEngine;
using TMPro;
using UnityEngine.UI;
using ProjectTime.Build;
using ProjectTime.Core;
using System;
using ProjectTime.Shielding;
using ProjectTime.Resources;

namespace ProjectTime.UI
{
    public class ShieldGeneratorBuildingUI : BuildingUI
    {
        [SerializeField] TextMeshProUGUI nameUI;
        [SerializeField] Slider integrityUI;
        [SerializeField] TextMeshProUGUI integrityTextUI;
        [SerializeField] Button expandShieldButton;
        [SerializeField] Button shrinkShieldButton;

        ShieldGenerator targetBuilding;

        private void Start()
        {
            player = GameObject.FindObjectOfType<Player>();
            closeButton.onClick.AddListener(CloseOnClick);
            expandShieldButton.onClick.AddListener(ExpandShields);
            shrinkShieldButton.onClick.AddListener(ShrinkShields);
            deleteBuildingButton.onClick.AddListener(DeleteBuilding);
            togglePowerButton.onClick.AddListener(TogglePower);
        }

        private void TogglePower()
        {
            targetBuilding.TogglePowered();
        }

        public override void DeleteBuilding()
        {
            ResourceManager.Instance.Refund(targetBuilding.BuildCost, .5f);
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

        private void CloseOnClick()
        {
            player.CloseUI();
        }

        public override void SetTarget(GameObject target)
        {
            targetBuilding = (ShieldGenerator)target.GetComponent<Building>();
        }

        private void OnGUI()
        {
            nameUI.text = targetBuilding.BuildingName;
            integrityTextUI.text = targetBuilding.Health.ToString();
            integrityUI.maxValue = targetBuilding.MaxHealth;
            integrityUI.value = targetBuilding.Health;
            powerText.text = targetBuilding.IsPowered.ToString();
        }
    }
}