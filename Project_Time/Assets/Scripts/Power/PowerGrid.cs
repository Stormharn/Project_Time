using System.Collections.Generic;

namespace ProjectTime.Power
{
    public sealed class PowerGrid
    {
        private static readonly PowerGrid instance = new PowerGrid();
        private static List<PowerGenerator> powerGenerators;

        static PowerGrid()
        {
            powerGenerators = new List<PowerGenerator>();
        }

        private PowerGrid() { }

        public static PowerGrid Instance
        {
            get { return instance; }
        }

        public List<PowerGenerator> GetGenerators()
        {
            return powerGenerators;
        }

        public void AddGenerator(PowerGenerator generator)
        {
            powerGenerators.Add(generator);
        }

        public void RemoveGenerator(PowerGenerator generator)
        {
            powerGenerators.Remove(generator);
        }

        public void UpdatePowerGrid()
        {
            foreach (var generator in powerGenerators)
            {
                generator.GeneratePower();
            }
        }
    }
}