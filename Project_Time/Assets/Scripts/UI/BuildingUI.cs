using UnityEngine;
using UnityEngine.UI;
using TMPro;
using ProjectTime.Core;
using ProjectTime.Build;
using ProjectTime.Population;

namespace ProjectTime.UI
{
    public abstract class BuildingUI : MonoBehaviour
    {
        [SerializeField] internal TextMeshProUGUI nameUI;
        [SerializeField] internal Slider integrityUI;
        [SerializeField] internal TextMeshProUGUI integrityTextUI;
        [SerializeField] internal Button closeButton;
        [SerializeField] internal Button deleteBuildingButton;
        [SerializeField] internal TextMeshProUGUI powerText;
        [SerializeField] internal TextMeshProUGUI workingText;
        [SerializeField] internal Button togglePowerButton;
        [SerializeField] internal Button addPopulationButton;
        [SerializeField] internal Button removePopulationButton;

        internal Player player;

        public abstract void SetTarget(GameObject target);

        public abstract void ToggleWorking();
        public abstract void DeleteBuilding();
        public abstract void AddPopulation();
        public abstract void RemovePopulation();

        internal void Setup()
        {
            player = GameObject.FindObjectOfType<Player>();
            closeButton.onClick.AddListener(CloseOnClick);
            deleteBuildingButton.onClick.AddListener(DeleteBuilding);
            togglePowerButton.onClick.AddListener(ToggleWorking);
            addPopulationButton.onClick.AddListener(AddPopulation);
            removePopulationButton.onClick.AddListener(RemovePopulation);
        }

        internal void CloseOnClick()
        {
            player.CloseUI();
        }

        internal void DrawGUI(Building targetBuilding)
        {
            nameUI.text = targetBuilding.BuildingName;
            var healthString = targetBuilding.Health.ToString();
            if (healthString.Length > 5)
                healthString = healthString.Substring(0, 5);
            integrityTextUI.text = healthString;
            integrityUI.maxValue = targetBuilding.MaxHealth;
            integrityUI.value = targetBuilding.Health;
            powerText.text = targetBuilding.HasPower.ToString();
            workingText.text = targetBuilding.IsWorking.ToString();

            if (PopulationManager.Instance.AvailablePopulation() > 0)
                addPopulationButton.interactable = true;
            else
                addPopulationButton.interactable = false;

            if (targetBuilding.GetPopulationTotal() > 0)
                removePopulationButton.interactable = true;
            else
                removePopulationButton.interactable = false;

        }
    }
}