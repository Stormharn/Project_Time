using UnityEngine;

namespace ProjectTime.HexGrid
{
    public class HexSpawner : MonoBehaviour
    {
        [SerializeField] int width = 6;
        [SerializeField] public int height = 6;
        [SerializeField] public HexCell cellPrefab;

        private void Awake()
        {
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

            HexCell cell = Instantiate<HexCell>(cellPrefab, position, Quaternion.identity, transform);

            HexManager.Instance.AddHex(cell);
        }
    }
}