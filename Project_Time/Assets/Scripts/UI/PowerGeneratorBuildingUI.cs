using UnityEngine;
using TMPro;
using UnityEngine.UI;
using ProjectTime.Build;
using ProjectTime.Core;
using System;
using ProjectTime.Resources;

namespace ProjectTime.UI
{
    public class PowerGeneratorBuildingUI : BuildingUI
    {
        [SerializeField] TextMeshProUGUI nameUI;
        [SerializeField] Slider integrityUI;
        [SerializeField] TextMeshProUGUI integrityTextUI;

        PowerGenerator targetBuilding;

        private void Start()
        {
            player = GameObject.FindObjectOfType<Player>();
            closeButton.onClick.AddListener(CloseOnClick);
            deleteBuildingButton.onClick.AddListener(DeleteBuilding);
            togglePowerButton.onClick.AddListener(TogglePower);
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

        public void TogglePower()
        {
            targetBuilding.GeneratorPowerToggle();
        }

        public override void SetTarget(GameObject target)
        {
            targetBuilding = (PowerGenerator)target.GetComponent<Building>();
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