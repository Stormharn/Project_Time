// using UnityEngine;
// using ProjectTime.HexGrid;
// using ProjectTime.Build;
// using System.Collections.Generic;
// using System.Collections;
// using System;
// using UnityEngine.UI;
// using ProjectTime.Resources;
// using ProjectTime.Core;

// // TODO add cooldown for extend/lower shields
// namespace ProjectTime.Shielding
// {
//     public class ShieldGenerator : Building, IBuildable
//     {
//         [SerializeField] Shield goodShieldPrefab;
//         [SerializeField] Shield fairShieldPrefab;
//         [SerializeField] Shield lowShieldPrefab;
//         [SerializeField] float shieldRegenDelay = 20f;
//         [SerializeField] int baseShieldMultiplier = 1;
//         [SerializeField] int shieldExtendMultiplier = 1;
//         List<HexCell> cellsInRange = new List<HexCell>();
//         [Range(0, 2)] int shieldExtendLevel = 0;
//         WaitForSeconds regenTime;

//         public int ShieldExtendLevel { get => shieldExtendLevel; }
//         public int BaseShieldMultiplier { get => baseShieldMultiplier; }
//         public int ShieldExtendMultiplier { get => shieldExtendMultiplier; }

//         public event Action onPeriodicRegen;

//         private void OnEnable()
//         {
//             regenTime = new WaitForSeconds(shieldRegenDelay);
//             ShieldManager.Instance.AddGenerator(this);
//         }

//         private void OnDisable()
//         {
//             ShieldManager.Instance.RemoveGenerator(this);
//             ShieldManager.Instance.TriggerRegen();
//         }

//         internal IEnumerator PeriodicRegen()
//         {
//             while (true)
//             {
//                 yield return regenTime;
//                 RegenerateShields();
//             }
//         }

//         internal void GenerateBaseShields()
//         {
//             GetCellsInRange(baseShieldMultiplier);
//             foreach (var cell in cellsInRange)
//             {
//                 if (CheckOldShield(cell))
//                     goodShieldPrefab.Spawn(cell.transform, this, cell, true);
//             }
//         }

//         public void RegenerateShields()
//         {
//             if (!hasPower || !isPowered) { return; }
//             if (onPeriodicRegen != null)
//                 onPeriodicRegen();
//             GenerateBaseShields();
//             ChangeShieldLevel(0);
//         }

//         private bool CheckOldShield(HexCell cell)
//         {
//             if (cell.HasShield)
//             {
//                 var oldShield = cell.GetShield();
//                 if (oldShield.IsBase) { return false; }
//                 oldShield.Remove();
//             }
//             return true;
//         }

//         public void ChangeShieldLevel(int change)
//         {
//             if (!hasPower || !isPowered) { return; }
//             if (change != 0)
//             {
//                 if (change > 0)
//                 {
//                     if (shieldExtendLevel == 2) { return; }
//                     shieldExtendLevel++;
//                 }
//                 else
//                 {
//                     if (shieldExtendLevel == 0) { return; }
//                     shieldExtendLevel--;
//                 }
//             }

//             foreach (var cell in cellsInRange)
//             {
//                 CheckOldShield(cell);
//             }

//             GetCellsInRange(baseShieldMultiplier + (shieldExtendLevel * ShieldExtendMultiplier));
//             foreach (var cell in cellsInRange)
//             {
//                 if (CheckOldShield(cell))
//                 {
//                     switch (shieldExtendLevel)
//                     {
//                         case 2:
//                             lowShieldPrefab.Spawn(cell.transform, this, cell, false);
//                             break;
//                         case 1:
//                             fairShieldPrefab.Spawn(cell.transform, this, cell, false);
//                             break;
//                         default:
//                             break;
//                     }
//                 }
//             }
//             if (change < 0)
//                 ShieldManager.Instance.TriggerRegen();
//         }

//         private void GetCellsInRange(int rangeMultiplier)
//         {
//             cellsInRange.Clear();
//             cellsInRange = HexManager.Instance.NearestCells(transform.position, rangeMultiplier);
//         }

//         public override void Cleanup()
//         {
//             GetCellsInRange(baseShieldMultiplier + (shieldExtendLevel * ShieldExtendMultiplier));

//             foreach (var cell in cellsInRange)
//             {
//                 if (cell.HasShield)
//                 {
//                     var removeShield = cell.GetShield();
//                     removeShield.Remove();
//                 }
//             }
//         }

//         public override void PowerUp()
//         {
//             RegenerateShields();
//         }

//         public override void PowerDown()
//         {
//             foreach (var cell in cellsInRange)
//             {
//                 if (cell.HasShield)
//                 {
//                     var removeShield = cell.GetShield();
//                     removeShield.Remove();
//                 }
//             }
//             ShieldManager.Instance.TriggerRegen();
//         }

//         public override void Build(HexCell hexCell)
//         {
//             hexCell.AddBuilding(this);
//             myCell = hexCell;
//             health = maxHealth;
//             isPowered = true;
//             hasPower = hexCell.HasPower;
//             PowerGrid.Instance.UpdatePowerGrid();
//             if (hasPower && isPowered)
//                 GenerateBaseShields();
//             StartCoroutine(nameof(PeriodicRegen));
//         }

//         public override void Remove(bool removeAll)
//         {
//             myCell.RemoveBuilding();
//             GameObject.FindObjectOfType<Player>().CloseUI();
//             Cleanup();
//             Destroy(this.gameObject);
//         }

//         public override void TogglePowered()
//         {
//             isPowered = !isPowered;
//             PowerGrid.Instance.UpdatePowerGrid();
//         }

//         public override void TakeDamage(float damage)
//         {
//             health -= damage;
//             if (health <= 0)
//                 Remove(true);
//         }

//         public override void Repair(float healing)
//         {
//             health += healing;
//             if (health > maxHealth)
//                 health = maxHealth;
//         }

//         public override bool IsShielded()
//         {
//             return myCell.HasShield;
//         }

//         public BuildCost GetBuildCost()
//         {
//             return buildCost;
//         }

//         public bool isBuildable()
//         {
//             return ResourceManager.Instance.CanAffordToBuild(buildCost);
//         }

//         public void SetBuildable()
//         {
//             GameObject.FindObjectOfType<BuildingSpawner>().SelectBuildingType(this);
//         }
//     }
// }