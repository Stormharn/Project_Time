﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using ProjectTime.Buildings;
using ProjectTime.HexGrid;
using ProjectTime.UI;
using ProjectTime.Shielding;

namespace ProjectTime.Core
{
    public class Player : MonoBehaviour
    {
        // Declarations
        #region Declarations
        [SerializeField] Bunker HQPrefab;
        [SerializeField] Image mainUI;
        [SerializeField] Image shieldUI;
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
            previousPanel = null;
            activePanel = mainUI;
            mainUI.gameObject.SetActive(true);
        }

        private void Start()
        {
            var homeCell = HexManager.Instance.ClosestCell(Vector3.zero);
            var startingBase = Instantiate(HQPrefab, homeCell.transform.position, Quaternion.identity, transform);
            startingBase.InitialBuilding(homeCell);
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
                        if (Input.GetKey(KeyCode.LeftShift)) { return; }
                        buildingSpawner.SelectBuildingType(null);
                        BackUI();
                        return;
                    }
                    else if (hexCell.CurrentBuilding != null)
                    {
                        OpenUI(hexCell.CurrentBuilding.buildingUI);
                        openedPanel.GetComponent<BuildingUI>().SetTarget(hexCell.CurrentBuilding.gameObject);
                        hexCell.CurrentBuilding.OnSelectionPlayAudio();
                    }
                    else if (hexCell.CurrentResource != null)
                    {
                        OpenUI(hexCell.CurrentResource.ResourceUI);
                        openedPanel.GetComponent<ResourceUI>().SetTarget(hexCell.CurrentResource.gameObject);
                    }
                    else
                    {
                        CloseUI();
                        CloseShieldUI();
                    }


                    if (hexCell.Shield != null)
                    {
                        OpenShieldUI(hexCell.Shield);
                    }


                }
            }
            else if (Input.GetMouseButtonDown(1))
            {
                BackUI();
                CloseUI();
                CloseShieldUI();
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
            CloseUI();
            openedPanel = Instantiate(UI, GameObject.FindGameObjectWithTag(UnityTags.MainUI.ToString()).transform);
        }

        public void OpenShieldUI(Shield shield)
        {
            shieldUI.gameObject.SetActive(false);
            shieldUI.GetComponent<ShieldUI>().SetTarget(shield);
            shieldUI.gameObject.SetActive(true);
        }

        public void CloseUI()
        {
            if (openedPanel == null) { return; }
            Destroy(openedPanel.gameObject);
            openedPanel = null;
        }

        public void CloseShieldUI()
        {
            shieldUI.gameObject.SetActive(false);
        }

        public void BackUI()
        {
            if (previousPanel == activePanel)
            {
                GoToMainMenu();
            }
            else
            {
                activePanel.gameObject.SetActive(false);
                activePanel = previousPanel;
                activePanel.gameObject.SetActive(true);
            }
        }

        public void GoToMainMenu()
        {
            activePanel.gameObject.SetActive(false);
            activePanel = mainUI;
            activePanel.gameObject.SetActive(true);
        }
        #endregion

        // Private Methods
        #region Private Methods
        #endregion
    }
}

