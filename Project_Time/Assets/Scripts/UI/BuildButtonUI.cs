using UnityEngine;
using UnityEngine.UI;
using TMPro;
using ProjectTime.Build;

namespace ProjectTime.UI
{
    public class BuildButtonUI : MonoBehaviour
    {
        [SerializeField] Button buildButton;
        [SerializeField] TextMeshProUGUI label;
        [SerializeField] TextMeshProUGUI woodCostUI;
        [SerializeField] TextMeshProUGUI stoneCostUI;
        [SerializeField] TextMeshProUGUI steelCostUI;
        [SerializeField] TextMeshProUGUI foodCostUI;
        [SerializeField] GameObject buildObject;

        IBuildable buildable;
        BuildCost buildCost;

        private void Awake()
        {
            buildable = buildObject.GetComponent<IBuildable>();
            if (buildable == null)
            {
                Debug.LogError("GameObject is not IBuildable");
            }
            buildButton.onClick.AddListener(Clicked);
        }

        private void Clicked()
        {
            buildable.BuildBuildable();
        }

        private void OnEnable()
        {
            buildCost = buildable.GetBuildCost();
        }

        private void OnGUI()
        {
            label.text = buildObject.name;
            gameObject.name = "Build " + buildObject.name + " Button";
            woodCostUI.text = buildCost.Wood.ToString();
            stoneCostUI.text = buildCost.Stone.ToString();
            steelCostUI.text = buildCost.Steel.ToString();
            foodCostUI.text = buildCost.Food.ToString();

            if (buildable.isBuildable())
                buildButton.interactable = true;
            else
                buildButton.interactable = false;
        }
    }
}