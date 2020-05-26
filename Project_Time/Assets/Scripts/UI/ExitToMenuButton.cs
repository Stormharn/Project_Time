using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitToMenuButton : MonoBehaviour
{
    // Declarations
    #region Declarations
    LevelManager levelManager;
    #endregion

    // Unity Methods
    #region Unity Methods
    private void Awake()
    {
        levelManager = GameObject.FindObjectOfType<LevelManager>();
        GetComponent<Button>().onClick.AddListener(ExitToMenu);
    }
    #endregion

    // Public Methods
    #region Public Methods

    #endregion

    // Private Methods
    #region Private Methods
    private void ExitToMenu()
    {
        levelManager.MainMenu();
    }

    #endregion
}
