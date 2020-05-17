using System.Collections.Generic;

namespace ProjectTime.Population
{
    public interface IPopulationContainer
    {
        bool AddPopulation(int count);
        bool RemovePopulation(int count);
        void AddPopulation(Citizen citizen);
        void RemovePopulation(Citizen citizen);
        List<Citizen> GetCitizens();
        int GetPopulationTotal();
    }
}