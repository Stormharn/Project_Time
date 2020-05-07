using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectTime.Build;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
using ProjectTime.HexGrid;
using ProjectTime.UI;

namespace ProjectTime.Core
{
    public class Player : MonoBehaviour
    {
        // Declarations
        #region Declarations
        [SerializeField] Image mainUI;
        [SerializeField] Image buildUI;
        [SerializeField] BuildingSpawner buildingSpawner;
        Image openedPanel;
        Image activePanel;
        Image previousPanel;
        Camera playerCam;
        #endregion

        // Unity Methods
        #region Unity Methods
        private void Awake()
        {
            playerCam = Camera.main;
            previousPanel = mainUI;
            activePanel = mainUI;
            mainUI.gameObject.SetActive(true);
        }

        private void Update()
        {
            if (HandleUIInput()) { return; }
            HandleGameplayInput();
        }

        private void HandleGameplayInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var inputRay = playerCam.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(inputRay, out var hit, 1000f))
                {
                    var hexCell = hit.transform.GetComponent<HexCell>();
                    if (hexCell == null) { return; }

                    if (buildingSpawner.CurrentBuilding != null)
                    {
                        buildingSpawner.PlaceBuilding(playerCam);
                        return;
                    }
                    else if (hexCell.CurrentBuilding != null)
                    {
                        OpenUI(hexCell.CurrentBuilding.buildingUI);
                        openedPanel.GetComponent<BuildingUI>().SetTarget(hexCell.CurrentBuilding.gameObject);
                        return;
                    }
                    else if (hexCell.CurrentResource != null)
                    {
                        ChangeUI(hexCell.CurrentResource.ResourceUI);
                        return;
                    }

                }
            }
            else if (Input.GetMouseButtonDown(1))
            {
                buildingSpawner.RemoveBuilding(playerCam);
            }
        }

        private bool HandleUIInput()
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return true;
            }
            return false;
        }
        #endregion

        // Public Methods
        #region Public Methods
        public void ChangeUI(Image newUIPanel)
        {
            previousPanel = activePanel;
            previousPanel.gameObject.SetActive(false);
            activePanel = newUIPanel;
            activePanel.gameObject.SetActive(true);
        }

        public void OpenUI(Image UI)
        {
            if (openedPanel != null)
                CloseUI();
            openedPanel = Instantiate(UI, GameObject.FindGameObjectWithTag(UnityTags.MainUI.ToString()).transform);
        }

        public void CloseUI()
        {
            Destroy(openedPanel.gameObject);
            openedPanel = null;
        }

        public void BackUI()
        {
            activePanel.gameObject.SetActive(false);
            activePanel = previousPanel;
            activePanel.gameObject.SetActive(true);
        }
        #endregion

        // Private Methods
        #region Private Methods
        #endregion
    }
}

