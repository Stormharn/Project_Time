using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectTime.Build;
using ProjectTime.HexGrid;
using ProjectTime.Resources;
using ProjectTime.Core;
using ProjectTime.Power;
using ProjectTime.Population;

namespace ProjectTime.Shielding
{
    public class ShieldGenerator : Building
    {
        [SerializeField] Shield shieldPrefab;
        [SerializeField] float shieldRegenDelay = 20f;
        [SerializeField] int baseShieldMultiplier = 1;
        [SerializeField] int shieldExtendMultiplier = 1;
        [SerializeField] float cooldownTime = 5f;
        List<HexCell> cellsInRange = new List<HexCell>();
        [Range(0, 2)] int shieldExtendLevel = 0;
        WaitForSeconds regenTime;
        float cooldownTimer;

        public int ShieldExtendLevel { get => shieldExtendLevel; }
        public int BaseShieldMultiplier { get => baseShieldMultiplier; }
        public int ShieldExtendMultiplier { get => shieldExtendMultiplier; }

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
            if (PopulationManager.Instance.AvailablePopulation() > 0)
            {
                var myCitizen = PopulationManager.Instance.GetAvailableCitizen();
                myWorkers.Add(myCitizen);
                PopulationManager.Instance.ToWork(myCitizen);
                isWorking = true;
            }
            hasPower = hexCell.HasPower;
            regenTime = new WaitForSeconds(shieldRegenDelay);
            cooldownTimer = Time.time;
            audioSource = GetComponent<AudioSource>();
            ShieldManager.Instance.AddGenerator(this);
            PowerGrid.Instance.UpdatePowerGrid();
            if (hasPower && isWorking)
                GenerateShields();
            StartCoroutine(nameof(PeriodicRegen));
        }

        public void InitializeStartBase(HexCell hexCell, Bunker newParent)
        {
            myCell = hexCell;
            isSubBuilding = true;
            parent = newParent;
            isWorking = false;
            hasPower = parent.HasPower;
            regenTime = new WaitForSeconds(shieldRegenDelay);
            cooldownTimer = Time.time;
            ShieldManager.Instance.AddGenerator(this);
            StartCoroutine(nameof(PeriodicRegen));
            // if (!newParent.IsHomeBase)
            newParent.AddPopulation(1);
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

        public IEnumerator PeriodicRegen()
        {
            while (true)
            {
                yield return regenTime;
                RegenerateShields();
            }
        }

        public void RegenerateShields()
        {
            if (hasPower && isWorking)
            {
                LowerShields();
                GenerateShields();
            }
        }

        public void ExpandShields()
        {
            if (isOnCooldown()) { return; }
            if (shieldExtendLevel >= 2) { return; }
            shieldExtendLevel++;
            RegenerateShields();
            cooldownTimer = Time.time;
        }

        public void ReduceShields()
        {
            if (isOnCooldown()) { return; }
            if (shieldExtendLevel <= 0) { return; }
            shieldExtendLevel--;
            RegenerateShields();
            cooldownTimer = Time.time;
        }

        public bool isOnCooldown()
        {
            return Time.time - cooldownTimer < cooldownTime;
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

        public override void ToggleWorking()
        {
            if (isWorking)
            {
                LowerShields();
                isWorking = !isWorking;
                PowerGrid.Instance.UpdatePowerGrid();
            }
            else
            {
                if (myWorkers.Count > 0)
                {
                    GenerateShields();
                    isWorking = !isWorking;
                    PowerGrid.Instance.UpdatePowerGrid();
                }
            }
        }

        public override void Cleanup()
        {
            LowerShields();
            KillCitizens(myWorkers);
        }

        public override void PowerUp()
        {
            hasPower = true;
            if (isWorking)
                GenerateShields();
        }

        public override void PowerDown()
        {
            hasPower = false;
            LowerShields();
        }
    }
}