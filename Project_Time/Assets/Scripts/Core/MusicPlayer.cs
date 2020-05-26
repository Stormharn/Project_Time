using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    // Declarations
    #region Declarations
    static MusicPlayer instance;
    #endregion

    // Unity Methods
    #region Unity Methods
    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else
        {
            Destroy(gameObject);
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
