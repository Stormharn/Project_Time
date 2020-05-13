using System;
using ProjectTime.Shielding;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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
            shieldStrengthTextUI.text = targetShield.ShieldHealth.ToString();
            shieldStrengthUI.maxValue = targetShield.BaseShieldHealth;
            shieldStrengthUI.value = targetShield.ShieldHealth;
        }
    }
}