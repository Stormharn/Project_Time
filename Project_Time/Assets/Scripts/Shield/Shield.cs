using System;
using System.Collections.Generic;
using ProjectTime.Build;
using ProjectTime.Core;
using ProjectTime.HexGrid;
using UnityEngine;

namespace ProjectTime.Shielding
{
    public class Shield : MonoBehaviour
    {
        [SerializeField] int baseShieldHealth = 100;
        [SerializeField] ShieldStatus status;
        [SerializeField] ShieldMaterial[] shieldMaterials;

        MeshRenderer meshRenderer;
        LinkedList<ShieldGenerator> generatedBy = new LinkedList<ShieldGenerator>();
        float shieldHealth;
        HexCell myHexCell = null;
        ShieldGenerator myParent = null;
        bool isBase = false;

        public bool IsBase { get => isBase; }
        public float ShieldHealth { get => shieldHealth; }
        public ShieldStatus ShieldStatus { get => status; }
        public int BaseShieldHealth { get => baseShieldHealth; }

        private void Awake()
        {
            meshRenderer = GetComponent<MeshRenderer>();
        }

        public void TakeDamage(float damage)
        {
            shieldHealth -= damage;
            if (shieldHealth <= 0)
                Remove();
        }

        public Shield InitializeShield(HexCell hexCell, ShieldGenerator parent)
        {
            myHexCell = hexCell;
            shieldHealth = baseShieldHealth;
            hexCell.AddShield(this);
            AddGenerator(parent, true);
            return this;
        }

        public void Remove()
        {
            myHexCell.RemoveShield();
            Destroy(this.gameObject);
        }

        public void AddGenerator(ShieldGenerator generator, bool addFirst)
        {
            if (addFirst)
                generatedBy.AddFirst(generator);
            else
                generatedBy.AddLast(generator);
        }

        public void RemoveGenerator(ShieldGenerator generator)
        {
            generatedBy.Remove(generator);
        }

        public ShieldGenerator CurrentParent()
        {
            return generatedBy.First.Value;
        }

        public int CurrentParentExtensionLevel()
        {
            return generatedBy.First.Value.ShieldExtendLevel;
        }

        public void UpdateShield(ShieldStatus newStatus)
        {
            for (int i = 0; i < shieldMaterials.Length; i++)
            {
                if (shieldMaterials[i].Status == newStatus)
                {
                    status = newStatus;
                    meshRenderer.material = shieldMaterials[i].Material;
                    UpdateHealth();
                    return;
                }
            }
        }

        private void UpdateHealth()
        {
            shieldHealth = baseShieldHealth;
            for (int i = 0; i < ShieldLevels.Instance.GetLevel(status); i++)
            {
                shieldHealth /= 2;
            }
        }

        public void RemoveParent()
        {
            generatedBy.RemoveFirst();
            if (generatedBy.Count == 0)
                Remove();
            else
            {
                var newStatus = generatedBy.First.Value.GetShieldStatus();
                UpdateShield(newStatus);
            }
        }
    }
}