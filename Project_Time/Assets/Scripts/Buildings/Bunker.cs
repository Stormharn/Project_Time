using UnityEngine;
using ProjectTime.Core;
using ProjectTime.HexGrid;
using ProjectTime.Shielding;
using ProjectTime.Resources;
using ProjectTime.Population;

namespace ProjectTime.Buildings
{
    [RequireComponent(typeof(ShieldGenerator))]
    public class Bunker : Building
    {
        bool isHomeBase = false;
        ShieldGenerator shieldGenerator;

        public bool IsHomeBase { get => isHomeBase; }

        public override void Build(HexCell hexCell)
        {
            hexCell.AddBuilding(this);
            myCell = hexCell;
            health = maxHealth;
            isWorking = true;
            hasPower = true;
            audioSource = GetComponent<AudioSource>();
            shieldGenerator = GetComponent<ShieldGenerator>();
            shieldGenerator.InitializeStartBase(hexCell, this);
        }

        public void InitialBuilding(HexCell hexCell)
        {
            buildingName = "Head-Quarters";
            gameObject.name = buildingName;
            isHomeBase = true;
            Build(hexCell);
            // AddPopulation(1);
        }

        public override void Remove(bool removeAll)
        {
            if (removeAll)
            {
                myCell.RemoveBuilding();
                GameObject.FindObjectOfType<Player>().CloseUI();
                Cleanup();
                Destroy(this.gameObject);
            }
        }

        public void ExpandShields()
        {
            shieldGenerator.ExpandShields();
        }

        public void ReduceShields()
        {
            shieldGenerator.ReduceShields();
        }

        public bool ShieldGeneratorIsOnCooldown()
        {
            return shieldGenerator.isOnCooldown();
        }

        public override void Cleanup()
        {
            shieldGenerator.Cleanup();
        }

        public override void PowerUp()
        {

        }

        public override void PowerDown()
        {

        }

        public override void ToggleWorking()
        {

        }

        public new bool AddPopulation(int count)
        {
            if (PopulationManager.Instance.AvailablePopulation() < count) { return false; }
            for (int i = 0; i < count; i++)
            {
                var myCitizen = PopulationManager.Instance.GetAvailableCitizen();
                myWorkers.Add(myCitizen);
                PopulationManager.Instance.ToWork(myCitizen);
                shieldGenerator.AddPopulation(myCitizen);
            }
            return true;
        }

        public new bool RemovePopulation(int count)
        {
            if (myWorkers.Count < count) { return false; }
            for (int i = 0; i < count; i++)
            {
                PopulationManager.Instance.ToAvailable(myWorkers[i]);
                shieldGenerator.RemovePopulation(myWorkers[i]);
                myWorkers.RemoveAt(i);
            }
            return true;
        }
    }
}