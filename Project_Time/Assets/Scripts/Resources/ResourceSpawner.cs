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
            foreach (var cell in hexCells)
            {
                if (Random.Range(1f, 100f) < 10 && cell != homeBase)
                {
                    curResource = GetRandomWeightedResource();
                    var hexCell = cell.GetComponent<HexCell>();
                    if (hexCell.IsAvailable)
                        curResource.Spawn(hexCell.transform, resourceParent, hexCell);
                }
            }
            hexCells = HexManager.Instance.AllCells();
            foreach (var cell in hexCells)
            {
                if (Random.Range(1f, 100f) < 15 && cell != homeBase && cell.IsAvailable)
                {
                    curResource = GetRandomWeightedResource();
                    var hexCell = cell.GetComponent<HexCell>();
                    if (hexCell.IsAvailable)
                        curResource.Spawn(hexCell.transform, resourceParent, hexCell);
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