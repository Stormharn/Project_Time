using UnityEngine;
using ProjectTime.HexGrid;
using ProjectTime.Build;
using System.Collections.Generic;
using System;

namespace ProjectTime.Shielding
{
    public class ShieldGenerator : Building
    {
        [SerializeField] Shield goodShieldPrefab;
        [SerializeField] Shield fairShieldPrefab;
        [SerializeField] Shield lowShieldPrefab;
        List<HexCell> cellsInRange = new List<HexCell>();
        int shieldLevel = 1;

        public event Action onRemoveShield;

        private void OnEnable()
        {
            GameObject.FindObjectOfType<ShieldManager>().onRegenShields += RegenerateShields;
        }

        private void OnDisable()
        {
            GameObject.FindObjectOfType<ShieldManager>().onRegenShields -= RegenerateShields;
            GameObject.FindObjectOfType<ShieldManager>().TriggerRegen();
        }

        public void Start()
        {
            GameObject.FindObjectOfType<ShieldManager>().RegisterNewGenerator(this);
            GenerateBaseShields();
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
            var hexManager = GameObject.FindGameObjectWithTag(UnityTags.HexManager.ToString()).GetComponent<HexManager>();
            var shieldRange = (Hex.innerRadius * 2 * rangeMultiplier) + 1f;
            cellsInRange = hexManager.NearestCells(transform.position, shieldRange);
        }

        public override void Cleanup()
        {
            print("cleanup is happeng in SheildGenerator");
            GetCellsInRange(shieldLevel);

            foreach (var cell in cellsInRange)
            {
                if (cell.HasShield)
                {
                    var removeShield = cell.GetShield();
                    removeShield.Remove();
                }
            }
            // onRemoveShield();
        }

        // TODO remove update which was added for testing
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                ChangeShieldLevel(1);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                ChangeShieldLevel(-1);
            }
            else if (Input.GetKeyDown(KeyCode.F5))
            {
                ChangeShieldLevel(0);
            }
        }
    }
}