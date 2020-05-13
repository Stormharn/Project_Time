using UnityEngine;
using TMPro;
using UnityEngine.UI;
using ProjectTime.Build;
using ProjectTime.Core;
using System;

namespace ProjectTime.UI
{
    public class StartBaseBuildingUI : BuildingUI
    {
        [SerializeField] TextMeshProUGUI nameUI;
        [SerializeField] Slider integrityUI;
        [SerializeField] TextMeshProUGUI integrityTextUI;
        [SerializeField] Button expandShieldButton;
        [SerializeField] Button shrinkShieldButton;

        StartBase targetBuilding;

        private void Start()
        {
            player = GameObject.FindObjectOfType<Player>();
            closeButton.onClick.AddListener(CloseOnClick);
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

        private void CloseOnClick()
        {
            player.CloseUI();
        }

        public override void SetTarget(GameObject target)
        {
            targetBuilding = (StartBase)target.GetComponent<Building>();
        }

        private void OnGUI()
        {
            nameUI.text = targetBuilding.BuildingName;
            integrityTextUI.text = targetBuilding.Health.ToString();
            integrityUI.maxValue = targetBuilding.MaxHealth;
            integrityUI.value = targetBuilding.Health;
            powerText.text = targetBuilding.IsPowered.ToString();
        }

        public override void DeleteBuilding()
        {

        }
    }
}