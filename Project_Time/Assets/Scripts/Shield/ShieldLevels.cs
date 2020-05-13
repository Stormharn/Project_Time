using System.Collections.Generic;

namespace ProjectTime.Shielding
{
    public sealed class ShieldLevels
    {
        private static readonly ShieldLevels instance = new ShieldLevels();
        private static Dictionary<int, ShieldStatus> levels;
        private static Dictionary<ShieldStatus, int> statuses;

        static ShieldLevels()
        {
            levels = new Dictionary<int, ShieldStatus>();
            statuses = new Dictionary<ShieldStatus, int>();
            levels.Add(0, ShieldStatus.Strong);
            levels.Add(1, ShieldStatus.Moderate);
            levels.Add(2, ShieldStatus.Minimal);
            statuses.Add(ShieldStatus.Strong, 0);
            statuses.Add(ShieldStatus.Moderate, 1);
            statuses.Add(ShieldStatus.Minimal, 2);
        }

        private ShieldLevels() { }

        public static ShieldLevels Instance
        {
            get { return instance; }
        }

        public ShieldStatus GetStatus(int key)
        {
            return (levels[key]);
        }

        public int GetLevel(ShieldStatus status)
        {
            return statuses[status];
        }
    }
}