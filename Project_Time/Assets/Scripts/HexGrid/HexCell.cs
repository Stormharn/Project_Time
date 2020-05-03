using UnityEngine;
using UnityEngine.UI;
using ProjectTime.Build;

namespace ProjectTime.HexGrid
{
    public class HexCell : MonoBehaviour
    {
        MeshCollider myCollider;
        bool isAvailable = true;
        Building currentBuilding = null;

        public bool IsAvailable { get => isAvailable; }

        private void Start()
        {
            GetComponent<MeshFilter>().mesh = GameObject.FindObjectOfType<HexMesh>().hexMesh;
            myCollider = GetComponent<MeshCollider>();

            myCollider.sharedMesh = GetComponent<MeshFilter>().mesh;
        }

        private void OnMouseEnter()
        {
            gameObject.layer = 9;
        }

        private void OnMouseExit()
        {
            gameObject.layer = 1;
        }

        public void AddBuilding(Building newBuilding)
        {
            if (!isAvailable) { return; }
            isAvailable = false;
            currentBuilding = newBuilding;
        }

        public void RemoveBuilding()
        {
            if (isAvailable) { return; }
            currentBuilding.Remove();
            currentBuilding = null;
            isAvailable = true;
        }
    }
}