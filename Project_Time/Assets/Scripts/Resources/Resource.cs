using System;
using ProjectTime.HexGrid;
using UnityEngine;

namespace ProjectTime.Resources
{
    public class Resource : MonoBehaviour
    {
        [SerializeField] ResourceTypes resourceType;
        [SerializeField] float resourceAmount = 100f;
        [SerializeField] float gatherAmount = 10f;
        HexCell myHexCell = null;

        public ResourceTypes ResourceType { get => resourceType; }
        public event Action<Resource> onResourceEmpty;

        public void Spawn(Transform resourceLocation, Transform parent, HexCell hexCell)
        {
            var newResource = Instantiate(this, resourceLocation.position, Quaternion.identity, parent);
            newResource.myHexCell = hexCell;
            hexCell.AddResource(newResource);
        }

        public float Gather(float gatherRate)
        {
            var gather = gatherAmount * gatherRate;
            if (resourceAmount >= gather)
                resourceAmount -= gather;
            else
            {
                gather = resourceAmount;
                resourceAmount = 0;
                onResourceEmpty(this);
                Invoke(nameof(Remove), 5f);
            }
            return gather;
        }

        public void Remove()
        {
            myHexCell.RemoveResource();
            Destroy(this.gameObject);
        }
    }
}