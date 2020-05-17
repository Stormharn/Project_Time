using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectTime.Population
{
    public sealed class PopulationManager
    {
        private static readonly PopulationManager instance = new PopulationManager();
        private static int totalPopulation;
        private static int workingPopulation;
        private static int availablePopulation;
        private static List<Citizen> allCitizens;
        private static List<Citizen> availableCitizens;

        static PopulationManager()
        {
            allCitizens = new List<Citizen>();
            availableCitizens = new List<Citizen>();
        }

        private PopulationManager() { }

        public static PopulationManager Instance
        {
            get { return instance; }
        }


        public Citizen CreatePopulation()
        {
            totalPopulation++;
            availablePopulation++;
            var newCitizen = new Citizen();
            allCitizens.Add(newCitizen);
            availableCitizens.Add(newCitizen);
            return newCitizen;
        }

        public List<Citizen> CreatePopulation(int count)
        {
            totalPopulation += count;
            availablePopulation += count;
            var newCitizens = new List<Citizen>();
            for (int i = 0; i < count; i++)
            {
                var newCitizen = new Citizen();
                allCitizens.Add(newCitizen);
                availableCitizens.Add(newCitizen);
                newCitizens.Add(newCitizen);
            }
            return newCitizens;
        }

        public Citizen GetAvailableCitizen()
        {
            if (availableCitizens.Count > 0)
                return availableCitizens[0];
            else
                return null;
        }

        public void ToWork(Citizen citizen)
        {
            workingPopulation++;
            availablePopulation--;
            availableCitizens.Remove(citizen);
        }

        public void ToWork(int count, List<Citizen> citizens)
        {
            workingPopulation += count;
            availablePopulation -= count;
            for (int i = 0; i < count; i++)
            {
                availableCitizens.Remove(citizens[i]);
            }
        }

        public void ToAvailable(Citizen citizen)
        {
            availablePopulation++;
            workingPopulation--;
            availableCitizens.Add(citizen);
        }

        public void ToAvailable(int count, List<Citizen> citizens)
        {
            availablePopulation += count;
            workingPopulation -= count;
            for (int i = 0; i < count; i++)
            {
                availableCitizens.Add(citizens[i]);
            }
        }

        public int AvailablePopulation()
        {
            return availablePopulation;
        }

        public int WorkingPopulation()
        {
            return workingPopulation;
        }

        public int TotalPopulation()
        {
            return totalPopulation;
        }

        public void KillCitizen(Citizen citizen)
        {
            if (availableCitizens.Contains(citizen))
            {
                availableCitizens.Remove(citizen);
                availablePopulation--;
            }
            else
                workingPopulation--;
            totalPopulation--;
        }
    }
}