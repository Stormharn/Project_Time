using UnityEngine;

namespace ProjectTime.HexGrid
{
    [ExecuteAlways]
    public class SnapToHexGrid : MonoBehaviour
    {
        Transform myTransform;

        private void Awake()
        {
            myTransform = gameObject.transform;
        }

        private void Update()
        {
            SnapToGrid();
        }

        private void SnapToGrid()
        {
            var newPosition = NearestHexPoint(myTransform.position);
            myTransform.position = newPosition;
        }

        private Vector3 NearestHexPoint(Vector3 position)
        {
            var xInt = Mathf.RoundToInt((position.x - (position.z * .5f) + (position.z / 2)) / (Hex.innerRadius * 2f));
            var zInt = Mathf.RoundToInt(position.z / (Hex.outerRadius * 1.5f));
            var x = (xInt + (zInt * .5f) - (zInt / 2)) * (Hex.innerRadius * 2f);
            var z = zInt * (Hex.outerRadius * 1.5f);
            return new Vector3(x, 0, z);
        }
    }
}