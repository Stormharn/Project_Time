using System.Collections;
using System.Collections.Generic;
using ProjectTime.Population;
using UnityEngine;

public class Testing : MonoBehaviour
{
    // Declarations
    #region Declarations
    GameObject mainUI;
    #endregion

    // Unity Methods
    #region Unity Methods
    private void Start()
    {
        Time.timeScale = 0;
        // mainUI = GameObject.FindGameObjectWithTag(UnityTags.MainUI.ToString());
        // mainUI.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            ProjectTime.Resources.ResourceManager.Instance.AddMaxResourceAll(1000);
            ProjectTime.Resources.ResourceManager.Instance.AddResourceAll(1000);
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            PopulationManager.Instance.CreatePopulation();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            mainUI.SetActive(true);
            Time.timeScale = 1;
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
