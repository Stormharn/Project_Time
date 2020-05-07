using ProjectTime.Build;
using ProjectTime.HexGrid;
using UnityEngine;

namespace ProjectTime.Shielding
{
    public class Shield : MonoBehaviour
    {
        [SerializeField] int baseShieldStrength = 10;
        int shieldStrength;
        HexCell myHexCell = null;
        ShieldGenerator myParent = null;
        bool isBase = false;

        public bool IsBase { get => isBase; }

        public void TakeDamage(int damage)
        {
            shieldStrength -= damage;
            if (shieldStrength <= 0)
                Remove();
        }

        public void Spawn(Transform shieldLocation, ShieldGenerator parent, HexCell hexCell, bool isBaseShield)
        {
            var newShield = Instantiate(this, shieldLocation.position, Quaternion.identity, parent.transform);
            newShield.myHexCell = hexCell;
            newShield.myParent = parent;
            newShield.isBase = isBaseShield;
            newShield.shieldStrength = newShield.baseShieldStrength;
            hexCell.AddShield(newShield);
            if (isBaseShield)
                parent.onPeriodicRegen += newShield.RefreshShieldStrength;
        }

        public void Remove()
        {
            myHexCell.RemoveShield();
            myParent.onPeriodicRegen -= RefreshShieldStrength;
            if (this.gameObject != null)
                Destroy(this.gameObject);
        }

        private void RefreshShieldStrength()
        {
            shieldStrength = baseShieldStrength;
        }

        //TODO remove update added for testing
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                TakeDamage(10);
        }
    }
}