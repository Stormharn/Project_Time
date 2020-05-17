using UnityEngine;
using UnityEngine.UI;
using TMPro;
using ProjectTime.Shielding;

namespace ProjectTime.UI
{
    public class ShieldUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI shieldLevelUI;
        [SerializeField] Slider shieldStrengthUI;
        [SerializeField] TextMeshProUGUI shieldStrengthTextUI;

        Shield targetShield;

        public void SetTarget(Shield shield)
        {
            targetShield = shield;
        }

        private void OnGUI()
        {
            shieldLevelUI.text = targetShield.ShieldStatus.ToString();
            var healthString = targetShield.ShieldHealth.ToString();
            if (healthString.Length > 4)
                healthString = healthString.Substring(0, 4);
            shieldStrengthTextUI.text = healthString;
            shieldStrengthUI.maxValue = targetShield.BaseShieldHealth;
            shieldStrengthUI.value = targetShield.ShieldHealth;
        }
    }
}