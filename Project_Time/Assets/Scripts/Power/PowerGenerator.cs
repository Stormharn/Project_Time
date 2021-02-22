using System.Collections.Generic;
using UnityEngine;
using ProjectTime.HexGrid;
using ProjectTime.Core;
using ProjectTime.Resources;
using ProjectTime.Buildings;
using ProjectTime.Population;

namespace ProjectTime.Power
{
    public class PowerGenerator : Building
    {
        [SerializeField] int generatorRange = 1;
        [SerializeField] float powerDraw;
        [SerializeField] float maxPower;

        List<HexCell> cellsInRange = new List<HexCell>();

        private void OnEnable()
        {
            PowerGrid.Instance.AddGenerator(this);
        }

        private void OnDisable()
        {
            PowerGrid.Instance.RemoveGenerator(this);
        }

        public void GeneratePower()
        {
            powerDraw = 0;
            if (isWorking)
            {
                foreach (var cell in cellsInRange)
                {
                    if (cell.CurrentBuilding == null) { continue; }
                    if (cell.CurrentBuilding.IsWorking)
                    {
                        powerDraw += cell.CurrentBuilding.RequiredPower;
                    }
                }
                CheckOverload();
            }
        }

        private void CheckOverload()
        {
            if (powerDraw > maxPower && isWorking)
            {
                PowerDownCells();
                ToggleWorking();
            }
            else if (powerDraw <= maxPower && !isWorking)
            {
                PowerUpCells();
                ToggleWorking();
            }
        }

        public void GeneratorPowerToggle()
        {
            if (!isWorking)
                GeneratePower();
            else
            {
                PowerDownCells();
                ToggleWorking();
            }

        }

        private void PowerUpCells()
        {
            foreach (var cell in cellsInRange)
            {
                cell.PowerUp(this);
            }
        }

        private void PowerDownCells()
        {
            foreach (var cell in cellsInRange)
            {
                cell.PowerDown(this);
            }
        }

        public override void Build(HexCell hexCell)
        {
            hexCell.AddBuilding(this);
            myCell = hexCell;
            health = maxHealth;
            if (PopulationManager.Instance.AvailablePopulation() > 0)
            {
                var myCitizen = PopulationManager.Instance.GetAvailableCitizen();
                myWorkers.Add(myCitizen);
                PopulationManager.Instance.ToWork(myCitizen);
                isWorking = true;
            }
            hasPower = true;
            audioSource = GetComponent<AudioSource>();
            cellsInRange = HexManager.Instance.NearestCells(transform.position, generatorRange);
            if (isWorking)
                PowerUpCells();
        }

        public override void Remove(bool removeAll)
        {
            myCell.RemoveBuilding();
            GameObject.FindObjectOfType<Player>().CloseUI();
            Cleanup();
            Destroy(this.gameObject);
        }

        public override void ToggleWorking()
        {
            if ((myWorkers.Count > 0 && !isWorking) || isWorking)
                isWorking = !isWorking;
            if (isWorking)
                PowerUpCells();
            else
                PowerDownCells();
        }

        public override void Cleanup()
        {
            KillCitizens(myWorkers);
            PowerDownCells();
        }

        public override void PowerUp()
        {
        }

        public override void PowerDown()
        {
        }
    }
}