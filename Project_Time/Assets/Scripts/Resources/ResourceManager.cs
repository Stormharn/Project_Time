using System;
using ProjectTime.Build;
using ProjectTime.Resources;
using UnityEngine;

namespace ProjectTime.Resources
{
    public class ResourceManager : MonoBehaviour
    {
        float currentWood = 0;
        float maxWood = 0;
        float currentStone = 0;
        float maxStone = 0;
        float currentSteel = 0;
        float maxSteel = 0;

        GameObject buildingParent;
        ResourceBuilding[] buildings;

        private void Awake()
        {
            buildingParent = GameObject.FindGameObjectWithTag(UnityTags.BuildingsParent.ToString());
            UpdateBuildings();
        }

        public void UpdateResources()
        {
            ClearValues();
            foreach (var building in buildings)
            {
                if (building.ResourceType == ResourceTypes.Wood)
                {
                    currentWood += building.CurrentResourceAmount;
                    maxWood += building.ResourceCapacity;
                }
                if (building.ResourceType == ResourceTypes.Stone)
                {
                    currentStone += building.CurrentResourceAmount;
                    maxStone += building.ResourceCapacity;
                }
                if (building.ResourceType == ResourceTypes.Steel)
                {
                    currentSteel += building.CurrentResourceAmount;
                    maxSteel += building.ResourceCapacity;
                }
            }
        }

        private void ClearValues()
        {
            currentWood = 0;
            currentSteel = 0;
            currentStone = 0;
            maxWood = 0;
            maxStone = 0;
            maxSteel = 0;
        }

        public void UpdateBuildings()
        {
            buildings = buildingParent.GetComponentsInChildren<ResourceBuilding>();
            UpdateResources();
        }

        public (float current, float max) WoodStats()
        {
            return (currentWood, maxWood);
        }

        public (float current, float max) StoneStats()
        {
            return (currentStone, maxStone);
        }

        public (float current, float max) SteelStats()
        {
            return (currentSteel, maxSteel);
        }
    }
}