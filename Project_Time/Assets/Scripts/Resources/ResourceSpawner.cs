using System.Collections.Generic;
using UnityEngine;
using ProjectTime.HexGrid;

namespace ProjectTime.Resources
{
    public class ResourceSpawner : MonoBehaviour
    {
        // Declarations
        #region Declarations
        [SerializeField] WeightedResource[] weightedResources;
        Resource curResource = null;
        List<WeightedResource> resourceTypes = new List<WeightedResource>();
        Transform resourceParent;
        HexCell homeBase;
        #endregion

        // Unity Methods
        #region Unity Methods
        private void Awake()
        {
            foreach (var weightedResource in weightedResources)
            {
                resourceTypes.Add(weightedResource);
            }
            resourceParent = GameObject.FindGameObjectWithTag(UnityTags.ResourceParent.ToString()).transform;
        }

        private void Start()
        {
            homeBase = HexManager.Instance.ClosestCell(Vector3.zero);
            var hexCells = HexManager.Instance.NearestCells(Vector3.zero, 2);
            foreach (var resource in resourceTypes)
            {
                var thisCell = hexCells[Random.Range(0, hexCells.Count)];
                while (thisCell == homeBase || !thisCell.IsAvailable)
                {
                    thisCell = hexCells[Random.Range(0, hexCells.Count)];
                }
                resource.resourceType.Spawn(thisCell.transform, resourceParent, thisCell);
            }
            hexCells = HexManager.Instance.AllCells();
            foreach (var cell in hexCells)
            {
                if (Random.Range(1f, 100f) < 15 && cell != homeBase && cell.IsAvailable)
                {
                    curResource = GetRandomWeightedResource();
                    if (cell.IsAvailable)
                        curResource.Spawn(cell.transform, resourceParent, cell);
                }
            }
        }

        #endregion

        // Public Methods
        #region Public Methods

        #endregion

        // Private Methods
        #region Private Methods
        private Resource GetRandomWeightedResource()
        {
            var total = 0f;
            for (int i = 0; i < resourceTypes.Count; i++)
            {
                total += resourceTypes[i].weight;
            }
            var valueToBeat = Random.Range(0f, total);
            var count = 0f;
            foreach (var weightedResource in weightedResources)
            {
                count += weightedResource.weight;
                if (count > valueToBeat)
                    return weightedResource.resourceType;
            }
            return null;
        }
        #endregion
    }
}