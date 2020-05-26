using System.Collections;
using System.Collections.Generic;
using ProjectTime.Build;
using ProjectTime.Population;
using ProjectTime.Resources;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Declarations
    #region Declarations
    [SerializeField] int startingResources;
    [SerializeField] int startingResourceCapacity;
    [SerializeField] int startingPopulation;
    [SerializeField] GameObject gameOverUI;
    #endregion

    // Unity Methods
    #region Unity Methods
    private void Awake()
    {
        for (int i = 0; i < startingPopulation; i++)
        {
            PopulationManager.Instance.CreatePopulation();
        }
        ResourceManager.Instance.AddResourceAll(startingResources);
        ResourceManager.Instance.AddMaxResourceAll(startingResourceCapacity);
    }

    private void Update()
    {
        var bunkers = GameObject.FindObjectsOfType<Bunker>();
        if (PopulationManager.Instance.TotalPopulation() == 0 || bunkers.Length == 0)
        {
            gameOverUI.gameObject.SetActive(true);
            gameOverUI.GetComponent<GameOverUI>().GameOver(false);
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
