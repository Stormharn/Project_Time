using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectTime.HexGrid;

namespace ProjectTime.Build
{
    public class BuildingSpawner : MonoBehaviour
    {
        // Declarations
        #region Declarations
        [SerializeField] Building[] allBuildingTypes;
        List<Building> availableBuildingTypes = new List<Building>();
        Building currentBuilding = null;
        Transform buildingsParent;
        #endregion

        // Unity Methods
        #region Unity Methods
        #endregion

        // Public Methods
        #region Public Methods
        private void Awake()
        {
            foreach (var building in allBuildingTypes)
            {
                availableBuildingTypes.Add(building);
            }
            buildingsParent = GameObject.FindGameObjectWithTag(UnityTags.BuildingsParent.ToString()).transform;
            currentBuilding = availableBuildingTypes[0];
        }
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
                    currentBuilding.Build(hit.transform, buildingsParent, hexCell);
                }
            }
        }

        public void RemoveBuilding(Camera playerCam)
        {
            var inputRay = playerCam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(inputRay, out var hit, 1000f))
            {
                var hexCell = hit.transform.GetComponent<HexCell>();
                if (hexCell == null) { return; }

                if (!hexCell.IsAvailable)
                {
                    hexCell.RemoveBuilding();
                }
            }
        }
        #endregion
    }
}
