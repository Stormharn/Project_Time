using System.Collections;
using System.Collections.Generic;
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
    }
    #endregion

    // Public Methods
    #region Public Methods

    #endregion

    // Private Methods
    #region Private Methods

    #endregion
}
