using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectTime.HexGrid;

namespace ProjectTime.Resources
{
    public class ResourceSpawner : MonoBehaviour
    {
        // Declarations
        #region Declarations
        [SerializeField] Resource[] allResourceTypes;
        Resource curResource = null;
        List<Resource> resourceTypes = new List<Resource>();
        Transform resourceParent;
        #endregion

        // Unity Methods
        #region Unity Methods
        private void Awake()
        {
            foreach (var resource in allResourceTypes)
            {
                resourceTypes.Add(resource);
            }
            resourceParent = GameObject.FindGameObjectWithTag(UnityTags.ResourceParent.ToString()).transform;
        }

        private void Start()
        {
            var hexCells = HexManager.Instance.AllCells();
            foreach (var cell in hexCells)
            {
                if (Random.Range(1f, 100f) < 20 && cell.IsAvailable)
                {
                    curResource = resourceTypes[Random.Range(0, resourceTypes.Count)];
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

        #endregion
    }
}