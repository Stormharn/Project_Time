using UnityEngine;
using TMPro;
using ProjectTime.Resources;
using ProjectTime.Core;
using UnityEngine.UI;

namespace ProjectTime.UI
{
    public class ResourceUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI nameUI;
        [SerializeField] TextMeshProUGUI amountUI;
        [SerializeField] Button closeButton;

        Resource targetResource;
        Player player;

        private void Start()
        {
            player = GameObject.FindObjectOfType<Player>();
            closeButton.onClick.AddListener(CloseOnClick);
        }

        private void CloseOnClick()
        {
            player.CloseUI();
        }

        public void SetTarget(GameObject target)
        {
            targetResource = target.GetComponent<Resource>();
        }

        private void OnGUI()
        {
            nameUI.text = targetResource.ResourceType.ToString();
            amountUI.text = string.Format("{0:0}", targetResource.ResourceAmount.ToString());
        }
    }
}