using System.Collections.Generic;

namespace ProjectTime.Shielding
{
    public sealed class ShieldManager
    {
        private static readonly ShieldManager instance = new ShieldManager();
        private static List<ShieldGenerator> shieldGenerators;

        static ShieldManager()
        {
            shieldGenerators = new List<ShieldGenerator>();
        }

        private ShieldManager() { }

        public static ShieldManager Instance
        {
            get { return instance; }
        }

        public static void Reinit()
        {
            shieldGenerators = new List<ShieldGenerator>();
        }

        public void AddGenerator(ShieldGenerator newGenerator)
        {
            shieldGenerators.Add(newGenerator);
        }

        public void RemoveGenerator(ShieldGenerator generator)
        {
            shieldGenerators.Remove(generator);
        }

        public void TriggerRegen()
        {
            foreach (var generator in shieldGenerators)
            {
                generator.RegenerateShields();
            }
        }
    }
}