using System.Collections.Generic;
using UnityEngine;


namespace ProjectTime.HexGrid
{
    public sealed class HexManager
    {
        private static readonly HexManager instance = new HexManager();
        private static List<HexCell> cells;

        static HexManager()
        {
            cells = new List<HexCell>();
        }

        private HexManager() { }

        public static HexManager Instance
        {
            get { return instance; }
        }

        public static void Reinit()
        {
            cells = new List<HexCell>();
        }

        public void AddHex(HexCell cell)
        {
            cells.Add(cell);
        }

        public List<HexCell> NearestCells(Vector3 position, float multiplier)
        {
            var range = (Hex.innerRadius * 2 * multiplier) + 1f;
            var list = new List<HexCell>();
            foreach (var cell in cells)
            {
                var distance = Vector3.Distance(cell.transform.position, position);
                if (distance <= range)
                    list.Add(cell);
            }
            return list;
        }

        public HexCell ClosestCell(Vector3 position)
        {
            HexCell closest = null;
            float closestDistance = Mathf.Infinity;
            foreach (var cell in cells)
            {
                var distance = Vector3.Distance(cell.transform.position, position);
                if (distance < closestDistance)
                {
                    closest = cell;
                    closestDistance = distance;
                }
            }
            return closest;
        }

        public HexCell RandomCell()
        {
            return cells[Random.Range(0, cells.Count)];
        }

        public List<HexCell> AllCells()
        {
            return cells;
        }
    }
}