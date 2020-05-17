using System.Collections;
using System.Collections.Generic;
using ProjectTime.Population;
using UnityEngine;

public class Testing : MonoBehaviour
{
    // Declarations
    #region Declarations

    #endregion

    // Unity Methods
    #region Unity Methods
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
    }
    #endregion

    // Public Methods
    #region Public Methods

    #endregion

    // Private Methods
    #region Private Methods

    #endregion
}
