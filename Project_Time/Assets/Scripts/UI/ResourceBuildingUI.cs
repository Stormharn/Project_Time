using UnityEngine;
using TMPro;
using UnityEngine.UI;
using ProjectTime.Build;
using ProjectTime.Core;
using System;
using ProjectTime.Resources;

namespace ProjectTime.UI
{
    public class ResourceBuildingUI : BuildingUI
    {
        [SerializeField] TextMeshProUGUI nameUI;
        [SerializeField] Slider integrityUI;
        [SerializeField] TextMeshProUGUI integrityTextUI;
        [SerializeField] TextMeshProUGUI typeUI;
        [SerializeField] TextMeshProUGUI amountUI;

        ResourceBuilding targetBuilding;

        private void Start()
        {
            player = GameObject.FindObjectOfType<Player>();
            closeButton.onClick.AddListener(CloseOnClick);
            deleteBuildingButton.onClick.AddListener(DeleteBuilding);
        }

        public override void DeleteBuilding()
        {
            GameObject.FindObjectOfType<ResourceManager>().Refund(targetBuilding.BuildCost, .5f);
            targetBuilding.Remove();
        }

        private void CloseOnClick()
        {
            player.CloseUI();
        }

        public override void SetTarget(GameObject target)
        {
            targetBuilding = (ResourceBuilding)target.GetComponent<Building>();
        }

        private void OnGUI()
        {
            nameUI.text = targetBuilding.BuildingName;
            integrityTextUI.text = targetBuilding.Integrity.ToString();
            integrityUI.maxValue = targetBuilding.MaxIntergity;
            integrityUI.value = targetBuilding.Integrity;
            typeUI.text = targetBuilding.ResourceType.ToString();
            amountUI.text = string.Format("{0:0} / {1:0}",
                            targetBuilding.CurrentResourceAmount.ToString(),
                            targetBuilding.ResourceCapacity.ToString());
        }
    }
}