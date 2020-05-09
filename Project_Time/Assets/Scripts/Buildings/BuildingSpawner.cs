using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectTime.HexGrid;
using ProjectTime.Resources;

namespace ProjectTime.Build
{
    public class BuildingSpawner : MonoBehaviour
    {
        // Declarations
        #region Declarations
        Building currentBuilding = null;
        ResourceManager resourceManager;
        Transform buildingsParent;

        public Building CurrentBuilding { get => currentBuilding; }
        #endregion

        // Unity Methods
        #region Unity Methods
        private void Awake()
        {
            buildingsParent = GameObject.FindGameObjectWithTag(UnityTags.BuildingsParent.ToString()).transform;
            resourceManager = GameObject.FindObjectOfType<ResourceManager>();

        }
        #endregion

        // Public Methods
        #region Public Methods
        #endregion

        // Private Methods
        #region Private Methods
        public void PlaceBuilding(Camera playerCam)
        {
            var inputRay = playerCam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(inputRay, out var hit, 1000f))
            {
                var hexCell = hit.transform.GetComponent<HexCell>();
                if (hexCell == null) { return; }

                if (hexCell.IsAvailable)
                {
                    if (currentBuilding && resourceManager.CanAffordToBuild(currentBuilding.BuildCost))
                    {
                        currentBuilding.Build(hit.transform, buildingsParent, hexCell);
                        resourceManager.Build(currentBuilding.BuildCost);
                    }
                }
            }
        }

        public void SelectBuildingType(Building newBuildingType)
        {
            if (newBuildingType == null || resourceManager.CanAffordToBuild(newBuildingType.BuildCost))
                currentBuilding = newBuildingType;
        }

        public void StopBuilding()
        {
            currentBuilding = null;
        }
        #endregion
    }
}
