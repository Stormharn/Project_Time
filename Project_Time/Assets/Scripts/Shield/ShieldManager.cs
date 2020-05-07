using System;
using UnityEngine;
using ProjectTime.Build;

namespace ProjectTime.Shielding
{
    public class ShieldManager : MonoBehaviour
    {
        public event Action onRegenShields;

        public void RegisterNewGenerator(ShieldGenerator newGenerator)
        {
            newGenerator.onRemoveShield += TriggerRegen;
        }

        public void TriggerRegen()
        {
            if (onRegenShields != null)
                onRegenShields();
        }
    }
}