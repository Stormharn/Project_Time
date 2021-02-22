using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectTime.Buildings;
using ProjectTime.Resources;

namespace ProjectTime.Population
{
    public class Citizen : MonoBehaviour, IBuildable
    {
        [SerializeField] BuildCost buildCost;
        string fullName;

        public string FullName { get => fullName; }

        private void Awake()
        {
            fullName = "citizen_" + Time.timeSinceLevelLoad.ToString();
        }

        public BuildCost GetBuildCost()
        {
            return buildCost;
        }

        public bool isBuildable()
        {
            return ResourceManager.Instance.CanAffordToBuild(buildCost);
        }

        public void BuildBuildable()
        {
            PopulationManager.Instance.CreatePopulation();
            ResourceManager.Instance.Build(buildCost);
        }
    }
}