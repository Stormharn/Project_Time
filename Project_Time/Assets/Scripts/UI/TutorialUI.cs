using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialUI : MonoBehaviour
{
    // Declarations
    #region Declarations
    [SerializeField] GameObject introUI;
    [SerializeField] Button backButton;
    #endregion

    // Unity Methods
    #region Unity Methods
    private void Awake()
    {
        backButton.onClick.AddListener(BackToIntro);
    }
    #endregion

    // Public Methods
    #region Public Methods

    #endregion

    // Private Methods
    #region Private Methods
    private void BackToIntro()
    {
        introUI.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    #endregion
}
