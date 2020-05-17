using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using ProjectTime.Population;

public class PopulationText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI totalPopText;
    [SerializeField] TextMeshProUGUI availablePopText;
    [SerializeField] TextMeshProUGUI workingPopText;

    private void OnGUI()
    {
        totalPopText.text = PopulationManager.Instance.TotalPopulation().ToString();
        availablePopText.text = PopulationManager.Instance.AvailablePopulation().ToString();
        workingPopText.text = PopulationManager.Instance.WorkingPopulation().ToString();
    }
}
