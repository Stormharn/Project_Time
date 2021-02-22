using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ProjectTime.HexGrid;
using ProjectTime.Population;
using ProjectTime.Resources;

namespace ProjectTime.Buildings
{
    public abstract class Building : MonoBehaviour, IBuildable, IPopulationContainer
    {
        [SerializeField] internal string buildingName;
        [SerializeField] internal float maxHealth;
        [SerializeField] internal float requiredPower;
        [SerializeField] internal Image buildingUI;
        [SerializeField] internal BuildCost buildCost;

        internal bool isSubBuilding = false;
        internal Building parent = null;
        internal bool isWorking;
        internal bool hasPower;
        internal float health;
        internal HexCell myCell;
        internal List<Citizen> myWorkers = new List<Citizen>();
        internal AudioSource audioSource;

        public string BuildingName { get => buildingName; }
        public float MaxHealth { get => maxHealth; }
        public float Health { get => health; }
        public bool HasPower { get => hasPower; }
        public bool IsWorking { get => isWorking; }
        public Image BuildingUI { get => buildingUI; }
        public BuildCost BuildCost { get => buildCost; }
        public float RequiredPower { get => requiredPower; }

        public abstract void Build(HexCell hexCell);
        public abstract void Remove(bool removeAll);
        public abstract void ToggleWorking();
        public abstract void Cleanup();
        public abstract void PowerUp();
        public abstract void PowerDown();

        public void TakeDamage(float damage)
        {
            if (isSubBuilding) { return; }
            health -= damage;
            if (health <= 0)
                Remove(true);
        }

        public void Repair(float healing)
        {
            if (isSubBuilding) { return; }
            health += healing;
            if (health > maxHealth)
                health = maxHealth;
        }

        public bool IsShielded()
        {
            if (isSubBuilding) { return parent.IsShielded(); }
            return myCell.HasShield;
        }


        public BuildCost GetBuildCost()
        {
            if (isSubBuilding) { return parent.GetBuildCost(); }
            return buildCost;
        }

        public bool isBuildable()
        {
            if (isSubBuilding) { return parent.isBuildable(); }
            return ResourceManager.Instance.CanAffordToBuild(buildCost);
        }

        public void BuildBuildable()
        {
            if (isSubBuilding) { return; }
            GameObject.FindObjectOfType<BuildingSpawner>().SelectBuildingType(this);
        }

        public bool AddPopulation(int count)
        {
            if (PopulationManager.Instance.AvailablePopulation() < count) { return false; }
            for (int i = 0; i < count; i++)
            {
                var myCitizen = PopulationManager.Instance.GetAvailableCitizen();
                myWorkers.Add(myCitizen);
                PopulationManager.Instance.ToWork(myCitizen);
            }
            if (myWorkers.Count > 0 && !isWorking)
                ToggleWorking();
            return true;
        }

        public void AddPopulation(Citizen citizen)
        {
            myWorkers.Add(citizen);
            if (myWorkers.Count > 0 && !isWorking)
                ToggleWorking();
        }

        public bool RemovePopulation(int count)
        {
            if (myWorkers.Count < count) { return false; }
            for (int i = 0; i < count; i++)
            {
                PopulationManager.Instance.ToAvailable(myWorkers[i]);
                myWorkers.RemoveAt(i);
            }
            if (myWorkers.Count == 0 && isWorking)
                ToggleWorking();
            return true;
        }

        public void RemovePopulation(Citizen citizen)
        {
            if (myWorkers.Contains(citizen))
                myWorkers.Remove(citizen);
            if (myWorkers.Count == 0 && isWorking)
                ToggleWorking();
        }

        public int GetPopulationTotal()
        {
            return myWorkers.Count;
        }

        public List<Citizen> GetCitizens()
        {
            return myWorkers;
        }

        public void KillCitizens(List<Citizen> citizens)
        {
            foreach (var citizen in citizens)
            {
                PopulationManager.Instance.KillCitizen(citizen);
            }
        }

        public void OnSelectionPlayAudio()
        {
            audioSource.Play();
        }

    }
}
