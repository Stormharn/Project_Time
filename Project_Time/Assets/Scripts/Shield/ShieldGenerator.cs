using UnityEngine;
using ProjectTime.HexGrid;
using ProjectTime.Build;
using System.Collections.Generic;
using System.Collections;
using System;

namespace ProjectTime.Shielding
{
    public class ShieldGenerator : Building
    {
        [SerializeField] Shield goodShieldPrefab;
        [SerializeField] Shield fairShieldPrefab;
        [SerializeField] Shield lowShieldPrefab;
        [SerializeField] float shieldRegenDelay = 20f;
        List<HexCell> cellsInRange = new List<HexCell>();
        int shieldLevel = 1;
        WaitForSeconds regenTime;
        HexManager hexManager;


        public event Action onRemoveShield;
        public event Action onPeriodicRegen;

        private void OnEnable()
        {
            GameObject.FindObjectOfType<ShieldManager>().onRegenShields += RegenerateShields;
        }

        private void OnDisable()
        {
            GameObject.FindObjectOfType<ShieldManager>().onRegenShields -= RegenerateShields;
            GameObject.FindObjectOfType<ShieldManager>().TriggerRegen();
        }

        private void Awake()
        {
            hexManager = GameObject.FindGameObjectWithTag(UnityTags.HexManager.ToString()).GetComponent<HexManager>();
            regenTime = new WaitForSeconds(shieldRegenDelay);
        }

        internal void Start()
        {
            GameObject.FindObjectOfType<ShieldManager>().RegisterNewGenerator(this);
            hasPower = myCell.HasPower;
            if (hasPower && isPowered)
                GenerateBaseShields();
            StartCoroutine(nameof(PeriodicRegen));
        }

        private IEnumerator PeriodicRegen()
        {
            while (true)
            {
                yield return regenTime;
                RegenerateShields();
            }
        }

        private void GenerateBaseShields()
        {
            GetCellsInRange(1);
            foreach (var cell in cellsInRange)
            {
                if (CheckOldShield(cell))
                    goodShieldPrefab.Spawn(cell.transform, this, cell, true);
            }
        }

        private void RegenerateShields()
        {
            if (!hasPower || !isPowered) { return; }
            if (onPeriodicRegen != null)
                onPeriodicRegen();
            GenerateBaseShields();
            ChangeShieldLevel(0);
        }

        private bool CheckOldShield(HexCell cell)
        {
            if (cell.HasShield)
            {
                var oldShield = cell.GetShield();
                if (oldShield.IsBase) { return false; }
                oldShield.Remove();
            }
            return true;
        }

        public void ChangeShieldLevel(int change)
        {
            if (change != 0)
            {
                if (change > 0)
                {
                    if (shieldLevel >= 3) { return; }
                    shieldLevel++;
                }
                else
                {
                    if (shieldLevel <= 1) { return; }
                    shieldLevel--;
                }
            }

            foreach (var cell in cellsInRange)
            {
                CheckOldShield(cell);
            }

            GetCellsInRange(shieldLevel);
            foreach (var cell in cellsInRange)
            {
                if (CheckOldShield(cell))
                {
                    switch (shieldLevel)
                    {
                        case 3:
                            lowShieldPrefab.Spawn(cell.transform, this, cell, false);
                            break;
                        case 2:
                            fairShieldPrefab.Spawn(cell.transform, this, cell, false);
                            break;
                        default:
                            break;
                    }
                }
            }
            if (change < 0)
                onRemoveShield();
        }

        private void GetCellsInRange(int rangeMultiplier)
        {
            cellsInRange.Clear();
            var shieldRange = (Hex.innerRadius * 2 * rangeMultiplier) + 1f;
            cellsInRange = hexManager.NearestCells(transform.position, shieldRange);
        }

        public override void Cleanup()
        {
            GetCellsInRange(shieldLevel);

            foreach (var cell in cellsInRange)
            {
                if (cell.HasShield)
                {
                    var removeShield = cell.GetShield();
                    removeShield.Remove();
                }
            }
        }

        // TODO remove update which was added for testing
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (hasPower && isPowered)
                    ChangeShieldLevel(1);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (hasPower && isPowered)
                    ChangeShieldLevel(-1);
            }
        }
    }
}