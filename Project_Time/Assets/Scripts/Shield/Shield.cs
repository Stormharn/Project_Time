using ProjectTime.HexGrid;
using UnityEngine;

namespace ProjectTime.Shielding
{
    public class Shield : MonoBehaviour
    {
        [SerializeField] Material[] shieldMaterials;
        int currentStrength = 3;
        MeshRenderer meshRenderer;

        private void Start()
        {
            meshRenderer = GetComponent<MeshRenderer>();
        }

        public void ChangeShieldStrength(int strength)
        {
            currentStrength = strength;
            meshRenderer.material = shieldMaterials[strength - 1];
        }

        public void Spawn(Transform shieldLocation, Transform parent, HexCell hexCell)
        {
            var newShield = Instantiate(this, shieldLocation.position, Quaternion.identity, parent);
        }
    }
}