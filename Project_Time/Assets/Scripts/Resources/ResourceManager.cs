using ProjectTime.Buildings;

namespace ProjectTime.Resources
{
    public sealed class ResourceManager
    {
        private static readonly ResourceManager instance = new ResourceManager();
        private static float currentWood;
        private static float maxWood;
        private static float currentStone;
        private static float maxStone;
        private static float currentSteel;
        private static float maxSteel;
        private static float currentFood;
        private static float maxFood;

        static ResourceManager()
        {

        }

        private ResourceManager() { }

        public static ResourceManager Instance
        {
            get { return instance; }
        }

        public static void Reinit()
        {
            currentFood = 0;
            currentSteel = 0;
            currentStone = 0;
            currentWood = 0;
            maxFood = 0;
            maxSteel = 0;
            maxStone = 0;
            maxWood = 0;
        }

        public void AddMaxResource(ResourceTypes type, float amount)
        {
            if (type == ResourceTypes.Wood)
            {
                maxWood += amount;
            }
            else if (type == ResourceTypes.Stone)
            {
                maxStone += amount;
            }
            else if (type == ResourceTypes.Steel)
            {
                maxSteel += amount;
            }
            else if (type == ResourceTypes.Food)
            {
                maxFood += amount;
            }
        }

        public void AddMaxResourceAll(float amount)
        {
            maxWood += amount;
            maxStone += amount;
            maxSteel += amount;
            maxFood += amount;
        }

        public void LowerMaxResource(ResourceTypes type, float amount)
        {
            if (type == ResourceTypes.Wood)
            {
                maxWood -= amount;
                if (maxWood < 0)
                    maxWood = 0;
            }
            else if (type == ResourceTypes.Stone)
            {
                maxStone -= amount;
                if (maxStone < 0)
                    maxStone = 0;
            }
            else if (type == ResourceTypes.Steel)
            {
                maxSteel -= amount;
                if (maxSteel < 0)
                    maxSteel = 0;
            }
            else if (type == ResourceTypes.Food)
            {
                maxFood -= amount;
                if (maxFood < 0)
                    maxFood = 0;
            }
        }

        public void AddResource(ResourceTypes type, float amount)
        {
            if (type == ResourceTypes.Wood)
            {
                currentWood += amount;
            }
            else if (type == ResourceTypes.Stone)
            {
                currentStone += amount;
            }
            else if (type == ResourceTypes.Steel)
            {
                currentSteel += amount;
            }
            else if (type == ResourceTypes.Food)
            {
                currentFood += amount;
            }
        }

        public void AddResourceAll(float amount)
        {
            currentWood += amount;
            currentStone += amount;
            currentSteel += amount;
            currentFood += amount;
        }

        public void LowerResource(ResourceTypes type, float amount)
        {
            if (type == ResourceTypes.Wood)
            {
                currentWood -= amount;
                if (currentWood < 0)
                    currentWood = 0;
            }
            else if (type == ResourceTypes.Stone)
            {
                currentStone -= amount;
                if (currentStone < 0)
                    currentStone = 0;
            }
            else if (type == ResourceTypes.Steel)
            {
                currentSteel -= amount;
                if (currentSteel < 0)
                    currentSteel = 0;
            }
            else if (type == ResourceTypes.Food)
            {
                currentFood -= amount;
                if (currentFood < 0)
                    currentFood = 0;
            }
        }

        public (float current, float max) GetWoodStats()
        {
            return (currentWood, maxWood);
        }

        public (float current, float max) GetStoneStats()
        {
            return (currentStone, maxStone);
        }

        public (float current, float max) GetSteelStats()
        {
            return (currentSteel, maxSteel);
        }

        public (float current, float max) GetFoodStats()
        {
            return (currentFood, maxFood);
        }

        public (float wood, float stone, float steel, float food) GetCurrentResources()
        {
            return (currentWood, currentStone, currentSteel, currentFood);
        }

        public (float wood, float stone, float steel, float food) GetMaxResources()
        {
            return (maxWood, maxStone, maxSteel, maxFood);
        }

        public bool CanAffordToBuild(BuildCost buildCost)
        {
            if (currentWood < buildCost.Wood)
                return false;
            else if (currentStone < buildCost.Stone)
                return false;
            else if (currentSteel < buildCost.Steel)
                return false;
            else if (currentFood < buildCost.Food)
                return false;

            return true;
        }

        public void Build(BuildCost buildCost)
        {
            currentWood -= buildCost.Wood;
            currentStone -= buildCost.Stone;
            currentSteel -= buildCost.Steel;
            currentFood -= buildCost.Food;
        }

        public void CancelBuild(BuildCost buildCost)
        {
            currentWood += buildCost.Wood;
            currentStone += buildCost.Stone;
            currentSteel += buildCost.Steel;
            currentFood += buildCost.Food;
        }

        public void Refund(BuildCost buildCost, float rebatePercent)
        {
            currentWood += buildCost.Wood * rebatePercent;
            currentStone += buildCost.Stone * rebatePercent;
            currentSteel += buildCost.Steel * rebatePercent;
            currentFood += buildCost.Food * rebatePercent;
        }
    }
}