using UnityEngine;
using ProjectTime.HexGrid;

namespace ProjectTime.Build
{
    public class Building : MonoBehaviour
    {
        public void Build(Transform buildLocation, Transform parent, HexCell hexCell)
        {
            var newBuilding = Instantiate(this, buildLocation.position, Quaternion.identity, parent);
            hexCell.AddBuilding(newBuilding);
        }

        public void Remove()
        {
            Destroy(this.gameObject);
        }
    }
}
