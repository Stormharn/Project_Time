using System;
using System.Collections;
using System.Collections.Generic;
using ProjectTime.HexGrid;
using UnityEngine;
using ProjectTime.Build;
using ProjectTime.Core;

namespace ProjectTime.Resources
{
    public class GathererBuilding : Building, IBuildable
    {
        [SerializeField] ResourceTypes resourceType;
        [SerializeField] float resourceCapacity = 100f;
        [SerializeField] [Range(1, 5)] int gatherRangeInHexes = 1;
        [SerializeField] float gatherRate = 1f;
        [SerializeField] float gatherFrequency = 5f;

        List<Resource> gatherableResources = new List<Resource>();
        List<Resource> removeResources = new List<Resource>();
        List<HexCell> cellsInRange = new List<HexCell>();
        float gatherRange;
        WaitForSeconds gatherDelay;

        public float ResourceCapacity { get => resourceCapacity; }
        public ResourceTypes ResourceType { get => resourceType; }

        private void Awake()
        {
            gatherDelay = new WaitForSeconds(gatherFrequency);
        }

        private void FindNearbyResources()
        {
            cellsInRange = HexManager.Instance.NearestCells(transform.position, gatherRangeInHexes);
            CheckForResources();
        }

        private void CheckForResources()
        {
            gatherableResources.Clear();
            foreach (var cell in cellsInRange)
            {
                if (cell.HasResource)
                {
                    var currentResource = cell.GetResource();
                    if (currentResource.ResourceType == resourceType)
                    {
                        gatherableResources.Add(currentResource);
                        currentResource.onResourceEmpty += ResourceEmpty;
                    }
                }
            }
        }

        private IEnumerator GatherResources()
        {
            while (true)
            {
                yield return gatherDelay;
                if (gatherableResources.Count > 0)
                {
                    foreach (var resource in removeResources)
                    {
                        gatherableResources.Remove(resource);
                    }
                    removeResources.Clear();
                    foreach (var resource in gatherableResources)
                    {
                        var result = AllResources(resourceType);
                        if (result.current < result.max)
                        {
                            var gathered = resource.Gather(gatherRate);
                            ResourceManager.Instance.AddResource(resourceType, gathered);

                        }
                    }
                }
            }
        }

        private (float current, float max) AllResources(ResourceTypes type)
        {
            if (type == ResourceTypes.Wood)
                return ResourceManager.Instance.GetWoodStats();
            else if (type == ResourceTypes.Stone)
                return ResourceManager.Instance.GetStoneStats();
            else if (type == ResourceTypes.Steel)
                return ResourceManager.Instance.GetSteelStats();

            return (0, 0);
        }

        private void ResourceEmpty(Resource removeResource)
        {
            removeResources.Add(removeResource);
        }

        public override void Cleanup()
        {
            ResourceManager.Instance.LowerMaxResource(resourceType, resourceCapacity);
        }

        public override void PowerUp()
        {

        }

        public override void PowerDown()
        {

        }

        public override void Build(HexCell hexCell)
        {
            hexCell.AddBuilding(this);
            myCell = hexCell;
            health = maxHealth;
            isPowered = true;
            hasPower = hexCell.HasPower;
            PowerGrid.Instance.UpdatePowerGrid();
            ResourceManager.Instance.AddMaxResource(resourceType, ResourceCapacity);
            FindNearbyResources();
            StartCoroutine(nameof(GatherResources));
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
    }
}