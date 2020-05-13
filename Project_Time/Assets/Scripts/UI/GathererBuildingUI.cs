using UnityEngine;
using TMPro;
using UnityEngine.UI;
using ProjectTime.Build;
using ProjectTime.Core;
using System;
using ProjectTime.Resources;

namespace ProjectTime.UI
{
    public class GathererBuildingUI : BuildingUI
    {
        [SerializeField] TextMeshProUGUI nameUI;
        [SerializeField] Slider integrityUI;
        [SerializeField] TextMeshProUGUI integrityTextUI;
        [SerializeField] TextMeshProUGUI typeUI;
        [SerializeField] TextMeshProUGUI amountUI;

        GathererBuilding targetBuilding;

        private void Start()
        {
            player = GameObject.FindObjectOfType<Player>();
            closeButton.onClick.AddListener(CloseOnClick);
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

        private void CloseOnClick()
        {
            player.CloseUI();
        }

        public override void SetTarget(GameObject target)
        {
            targetBuilding = (GathererBuilding)target.GetComponent<Building>();
        }

        private void OnGUI()
        {
            nameUI.text = targetBuilding.BuildingName;
            integrityTextUI.text = targetBuilding.Health.ToString();
            integrityUI.maxValue = targetBuilding.MaxHealth;
            integrityUI.value = targetBuilding.Health;
            typeUI.text = targetBuilding.ResourceType.ToString();
            amountUI.text = targetBuilding.ResourceCapacity.ToString();
            powerText.text = targetBuilding.IsPowered.ToString();
        }
    }
}