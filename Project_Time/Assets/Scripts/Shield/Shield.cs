using ProjectTime.Build;
using ProjectTime.HexGrid;
using UnityEngine;

namespace ProjectTime.Shielding
{
    public class Shield : MonoBehaviour
    {
        int shieldLevel = 1;
        HexCell myHexCell = null;
        ShieldGenerator myParent = null;
        bool isBase = false;

        public bool IsBase { get => isBase; }

        public void ChangeShieldStrength(int strength)
        {
            shieldLevel = strength;
        }

        public void Spawn(Transform shieldLocation, ShieldGenerator parent, HexCell hexCell, bool isBaseShield)
        {
            var newShield = Instantiate(this, shieldLocation.position, Quaternion.identity, parent.transform);
            newShield.myHexCell = hexCell;
            newShield.myParent = parent;
            newShield.isBase = isBaseShield;
            hexCell.AddShield(newShield);
        }

        public void Remove()
        {
            myHexCell.RemoveShield();
            if (this.gameObject != null)
                Destroy(this.gameObject);
        }
    }
}