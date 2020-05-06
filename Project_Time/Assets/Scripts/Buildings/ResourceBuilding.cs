using System;
using System.Collections;
using System.Collections.Generic;
using ProjectTime.HexGrid;
using UnityEngine;
using ProjectTime.Resources;

namespace ProjectTime.Build
{
    public class ResourceBuilding : Building
    {
        [SerializeField] ResourceTypes resourceType;
        [SerializeField] float currentResourceAmount = 0f;
        [SerializeField] float resourceCapacity = 100f;
        [SerializeField] [Range(1, 5)] int gatherRangeInHexes = 1;
        [SerializeField] float gatherRate = 1f;

        List<Resource> gatherableResources = new List<Resource>();
        List<Resource> removeResources = new List<Resource>();
        List<HexCell> cellsInRange = new List<HexCell>();
        float gatherRange;
        WaitForSeconds gatherDelay = new WaitForSeconds(10f);

        private void Start()
        {
            gatherRange = (Hex.innerRadius * 2 * gatherRangeInHexes) + 1f;
            FindNearbyResources();
            StartCoroutine(nameof(GatherResources));
        }

        private void FindNearbyResources()
        {
            var hexManager = GameObject.FindGameObjectWithTag(UnityTags.HexManager.ToString()).GetComponent<HexManager>();
            cellsInRange = hexManager.NearestCells(transform.position, gatherRange);
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
                if (gatherableResources.Count > 0)
                {
                    foreach (var resource in removeResources)
                    {
                        gatherableResources.Remove(resource);
                    }
                    removeResources.Clear();
                    foreach (var resource in gatherableResources)
                    {
                        if (currentResourceAmount < resourceCapacity)
                        {
                            var gathered = resource.Gather(gatherRate);
                            currentResourceAmount += gathered;
                        }
                    }
                }
                yield return gatherDelay;
            }
        }

        private void ResourceEmpty(Resource removeResource)
        {
            removeResources.Add(removeResource);
        }

        public override void Cleanup()
        {

        }
    }
}