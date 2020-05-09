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
        }

        public override void DeleteBuilding()
        {
            GameObject.FindObjectOfType<ResourceManager>().Refund(targetBuilding.BuildCost, .5f);
            targetBuilding.Remove();
        }


        private void ShrinkShields()
        {
            targetBuilding.ChangeShieldLevel(-1);
        }

        private void ExpandShields()
        {
            targetBuilding.ChangeShieldLevel(1);
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
            integrityTextUI.text = targetBuilding.Integrity.ToString();
            integrityUI.maxValue = targetBuilding.MaxIntergity;
            integrityUI.value = targetBuilding.Integrity;
        }
    }
}