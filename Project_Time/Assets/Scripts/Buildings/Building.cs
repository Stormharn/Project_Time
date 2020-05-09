﻿using UnityEngine;
using ProjectTime.HexGrid;
using UnityEngine.UI;
using ProjectTime.UI;
using ProjectTime.Core;

namespace ProjectTime.Build
{
    public abstract class Building : MonoBehaviour
    {
        [SerializeField] internal string buildingName;
        [SerializeField] internal float maxIntergity;
        [SerializeField] internal bool hasPower;
        [SerializeField] internal bool isPowered;
        [SerializeField] internal Image buildingUI;

        internal float integrity;
        internal HexCell myCell;

        public string BuildingName { get => buildingName; }
        public float MaxIntergity { get => maxIntergity; }
        public float Integrity { get => integrity; }
        public bool HasPower { get => hasPower; }
        public bool IsPowered { get => isPowered; }
        public Image BuildingUI { get => buildingUI; }

        public void Build(Transform buildLocation, Transform parent, HexCell hexCell)
        {
            var newBuilding = Instantiate(this, buildLocation.position, Quaternion.identity, parent);
            hexCell.AddBuilding(newBuilding);
            newBuilding.myCell = hexCell;
            newBuilding.integrity = maxIntergity;

        }

        public void Remove()
        {
            if (buildingName != "Head Quarters")
            {
                myCell.RemoveBuilding();
                GameObject.FindObjectOfType<Player>().CloseUI();
                Cleanup();
                Destroy(this.gameObject);
            }
        }

        public void Remove(bool removeAll)
        {
            if (removeAll)
            {
                myCell.RemoveBuilding();
                GameObject.FindObjectOfType<Player>().CloseUI();
                Cleanup();
                Destroy(this.gameObject);
            }
        }

        public void TogglePowered()
        {
            isPowered = !isPowered;
        }

        public void TakeDamage(float damage)
        {
            integrity -= damage;
            if (integrity <= 0)
                Remove(true);
        }

        public void Repair(float health)
        {
            integrity += health;
            if (integrity >= maxIntergity)
                integrity = maxIntergity;
        }

        public bool IsShielded()
        {
            return myCell.HasShield;
        }

        public abstract void Cleanup();
    }
}
