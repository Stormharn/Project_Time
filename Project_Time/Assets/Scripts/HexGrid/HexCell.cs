using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using ProjectTime.Build;
using ProjectTime.Resources;
using ProjectTime.Shielding;
using ProjectTime.Power;

namespace ProjectTime.HexGrid
{
    public class HexCell : MonoBehaviour
    {
        MeshCollider myCollider;
        bool isAvailable = true;
        bool hasResource = false;
        bool hasShield = false;
        bool hasPower = false;
        Building currentBuilding = null;
        Resource currentResource = null;
        Shield shield = null;
        List<PowerGenerator> poweredBy = new List<PowerGenerator>();

        public bool IsAvailable { get => isAvailable; }
        public bool HasResource { get => hasResource; }
        public bool HasShield { get => hasShield; }
        public bool HasPower { get => hasPower; }
        public Building CurrentBuilding { get => currentBuilding; }
        public Resource CurrentResource { get => currentResource; }
        public Shield Shield { get => shield; }

        private void Start()
        {
            // GetComponent<MeshFilter>().mesh = GameObject.FindObjectOfType<HexMesh>().hexMesh;
            myCollider = GetComponent<MeshCollider>();

            myCollider.sharedMesh = GetComponent<MeshFilter>().mesh;
        }

        private void OnMouseEnter()
        {
            if (EventSystem.current.IsPointerOverGameObject()) { return; }
            gameObject.layer = 9;
        }

        private void OnMouseOver()
        {
            if (EventSystem.current.IsPointerOverGameObject() && gameObject.layer != 1)
                gameObject.layer = 10;
        }

        private void OnMouseExit()
        {
            gameObject.layer = 10;
        }

        public void AddBuilding(Building newBuilding)
        {
            isAvailable = false;
            currentBuilding = newBuilding;
        }

        public void RemoveBuilding()
        {
            currentBuilding = null;
            isAvailable = true;
        }

        public void AddResource(Resource newResource)
        {
            isAvailable = false;
            hasResource = true;
            currentResource = newResource;
        }

        public Resource GetResource()
        {
            return currentResource;
        }

        public void RemoveResource()
        {
            isAvailable = true;
            hasResource = false;
            currentResource = null;
        }

        public void AddShield(Shield newShield)
        {
            hasShield = true;
            shield = newShield;
        }

        public void RemoveShield()
        {
            hasShield = false;
            shield = null;
        }

        public Shield GetShield()
        {
            return shield;
        }

        public void PowerUp(PowerGenerator generator)
        {
            hasPower = true;
            if (currentBuilding != null)
            {
                currentBuilding.hasPower = true;
                currentBuilding.PowerUp();
            }
            if (generator != null)
                poweredBy.Add(generator);
        }

        public void PowerDown(PowerGenerator generator)
        {
            poweredBy.Remove(generator);
            if (poweredBy != null)
            {
                hasPower = false;
                if (currentBuilding != null)
                {
                    currentBuilding.hasPower = false;
                    currentBuilding.PowerDown();
                }
            }
        }

        public int PoweredByCount()
        {
            return poweredBy.Count;
        }
    }
}