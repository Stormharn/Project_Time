using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectTime.Build;
using ProjectTime.HexGrid;
using ProjectTime.Resources;
using ProjectTime.Core;
using System;

namespace ProjectTime.Shielding
{
    public class ShieldGenerator : Building, IBuildable
    {
        [SerializeField] Shield shieldPrefab;
        [SerializeField] float shieldRegenDelay = 20f;
        [SerializeField] int baseShieldMultiplier = 1;
        [SerializeField] int shieldExtendMultiplier = 1;
        List<HexCell> cellsInRange = new List<HexCell>();
        [Range(0, 2)] int shieldExtendLevel = 0;
        WaitForSeconds regenTime;

        public int ShieldExtendLevel { get => shieldExtendLevel; }
        public int BaseShieldMultiplier { get => baseShieldMultiplier; }
        public int ShieldExtendMultiplier { get => shieldExtendMultiplier; }

        private void OnEnable()
        {
            regenTime = new WaitForSeconds(shieldRegenDelay);
            ShieldManager.Instance.AddGenerator(this);
        }

        private void OnDisable()
        {
            ShieldManager.Instance.RemoveGenerator(this);
            ShieldManager.Instance.TriggerRegen();
        }

        public override void Build(HexCell hexCell)
        {
            hexCell.AddBuilding(this);
            myCell = hexCell;
            health = maxHealth;
            isPowered = true;
            hasPower = hexCell.HasPower;
            PowerGrid.Instance.UpdatePowerGrid();
            if (hasPower && isPowered)
                GenerateShields();
            StartCoroutine(nameof(PeriodicRegen));
        }

        internal void GenerateShields()
        {
            cellsInRange.Clear();
            cellsInRange = HexManager.Instance.NearestCells(transform.position, (baseShieldMultiplier + (shieldExtendLevel * shieldExtendMultiplier)));
            foreach (var cell in cellsInRange)
            {
                var shield = cell.Shield;
                if (shield == null)
                {
                    Instantiate(shieldPrefab, cell.transform.position, Quaternion.identity, transform)
                    .InitializeShield(cell, this)
                    .UpdateShield(ShieldLevels.Instance.GetStatus(shieldExtendLevel));
                }
                else
                {
                    if (shield.CurrentParentExtensionLevel() <= shieldExtendLevel)
                    {
                        shield.AddGenerator(this, false);
                    }
                    else
                    {
                        shield.AddGenerator(this, true);
                        shield.UpdateShield(ShieldLevels.Instance.GetStatus(shieldExtendLevel));
                    }
                }
            }
        }

        private void LowerShields()
        {
            foreach (var cell in cellsInRange)
            {
                var shield = cell.Shield;
                if (shield == null) { continue; }
                if (shield.CurrentParent() == this)
                    shield.RemoveParent();
                else
                    shield.RemoveGenerator(this);
            }
        }

        internal IEnumerator PeriodicRegen()
        {
            while (true)
            {
                yield return regenTime;
                RegenerateShields();
            }
        }

        public void RegenerateShields()
        {
            if (hasPower && isPowered)
            {
                LowerShields();
                GenerateShields();
            }
        }

        public void ExpandShields()
        {
            if (shieldExtendLevel >= 2) { return; }
            shieldExtendLevel++;
            RegenerateShields();
        }

        public void ReduceShields()
        {
            if (shieldExtendLevel <= 0) { return; }
            shieldExtendLevel--;
            RegenerateShields();
        }

        public ShieldStatus GetShieldStatus()
        {
            return ShieldLevels.Instance.GetStatus(shieldExtendLevel);
        }

        public override void Remove(bool removeAll)
        {
            myCell.RemoveBuilding();
            GameObject.FindObjectOfType<Player>().CloseUI();
            Cleanup();
            Destroy(this.gameObject);
        }

        public override void TogglePowered()
        {
            if (isPowered)
                LowerShields();
            else
                GenerateShields();
            isPowered = !isPowered;
            PowerGrid.Instance.UpdatePowerGrid();
        }

        public override void TakeDamage(float damage)
        {
            health -= damage;
            if (health <= 0)
                Remove(true);
        }

        public override void Repair(float healing)
        {
            health += healing;
            if (health > maxHealth)
                health = maxHealth;
        }

        public override bool IsShielded()
        {
            return myCell.HasShield;
        }

        public BuildCost GetBuildCost()
        {
            return buildCost;
        }

        public bool isBuildable()
        {
            return ResourceManager.Instance.CanAffordToBuild(buildCost);
        }

        public void SetBuildable()
        {
            GameObject.FindObjectOfType<BuildingSpawner>().SelectBuildingType(this);
        }

        public override void Cleanup()
        {
            LowerShields();
        }

        public override void PowerUp()
        {
            GenerateShields();
        }

        public override void PowerDown()
        {
            LowerShields();
        }
    }
}