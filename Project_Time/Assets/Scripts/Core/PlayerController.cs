using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectTime.Build;

namespace ProjectTime.Core
{
    public class PlayerController : MonoBehaviour
    {
        // Declarations
        #region Declarations
        [SerializeField] BuildingManager buildingManager;
        Camera playerCam;
        #endregion

        // Unity Methods
        #region Unity Methods
        private void Awake()
        {
            playerCam = Camera.main;
        }
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                buildingManager.PlaceBuilding(playerCam);
            }
            else if (Input.GetMouseButtonDown(1))
            {
                buildingManager.RemoveBuilding(playerCam);
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
