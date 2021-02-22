using UnityEngine;
using ProjectTime.HexGrid;
using ProjectTime.Resources;

namespace ProjectTime.Buildings
{
    public class BuildingSpawner : MonoBehaviour
    {
        // Declarations
        #region Declarations
        Building currentBuilding = null;
        Transform buildingsParent;
        AudioSource audioSource;

        public Building CurrentBuilding { get => currentBuilding; }
        #endregion

        // Unity Methods
        #region Unity Methods
        private void Awake()
        {
            buildingsParent = GameObject.FindGameObjectWithTag(UnityTags.BuildingsParent.ToString()).transform;
            audioSource = GetComponent<AudioSource>();
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
                    if (currentBuilding && ResourceManager.Instance.CanAffordToBuild(currentBuilding.BuildCost))
                    {
                        var newBuilding = Instantiate(currentBuilding, hit.transform.position, Quaternion.identity, buildingsParent);
                        audioSource.Play();
                        newBuilding.Build(hexCell);
                        ResourceManager.Instance.Build(currentBuilding.BuildCost);
                    }
                }
            }
        }

        public void SelectBuildingType(Building newBuildingType)
        {
            if (newBuildingType == null || ResourceManager.Instance.CanAffordToBuild(newBuildingType.BuildCost))
                currentBuilding = newBuildingType;
        }

        public void StopBuilding()
        {
            currentBuilding = null;
        }
        #endregion
    }
}
