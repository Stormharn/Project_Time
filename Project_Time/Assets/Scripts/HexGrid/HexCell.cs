using UnityEngine;
using UnityEngine.UI;

namespace ProjectTime.HexGrid
{
    public class HexCell : MonoBehaviour
    {
        MeshCollider myCollider;

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
    }
}