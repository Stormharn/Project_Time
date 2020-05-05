using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectTime.HexGrid
{
    public class HexManager : MonoBehaviour
    {
        [SerializeField] int width = 6;
        [SerializeField] public int height = 6;
        [SerializeField] public HexCell cellPrefab;

        HexCell[] cells;

        void Awake()
        {
            cells = new HexCell[height * width];
            for (int z = 0, i = 0; z < height; z++)
            {
                for (int x = 0; x < width; x++)
                {
                    CreateCell(Mathf.RoundToInt(x - width / 2), Mathf.RoundToInt(z - height / 2), i++);
                }
            }
        }

        void CreateCell(int x, int z, int i)
        {
            Vector3 position;
            position.x = (x + z * 0.5f - z / 2) * (Hex.innerRadius * 2f);
            position.y = 0f;
            position.z = z * (Hex.outerRadius * 1.5f);

            HexCell cell = cells[i] = Instantiate<HexCell>(cellPrefab, position, Quaternion.identity, transform);
            if (position == Vector3.zero)
                cell.tag = UnityTags.StartBase.ToString();
        }

        public List<HexCell> NearestCells(Vector3 position, float range)
        {
            var list = new List<HexCell>();
            foreach (var cell in cells)
            {
                var distance = Vector3.Distance(cell.transform.position, position);
                if (distance <= range)
                    list.Add(cell);
            }
            return list;
        }
    }
}