using System;
using UnityEngine;
using UnityEngine.UI;
using ProjectTime.HexGrid;

namespace ProjectTime.Resources
{
    public class Resource : MonoBehaviour
    {
        [SerializeField] ResourceTypes resourceType;
        [SerializeField] float currentResourceAmount = 100f;
        [SerializeField] float minResourceAmount = 1000f;
        [SerializeField] float maxResourceAmount = 10000f;
        [SerializeField] float gatherAmount = 10f;
        [SerializeField] Image resourceUI;
        HexCell myHexCell = null;

        public ResourceTypes ResourceType { get => resourceType; }
        public Image ResourceUI { get => resourceUI; }
        public float CurrentResourceAmount { get => currentResourceAmount; }

        public event Action<Resource> onResourceEmpty;

        private void Start()
        {
            currentResourceAmount = Mathf.RoundToInt(UnityEngine.Random.Range(minResourceAmount, maxResourceAmount));
        }

        public void Spawn(Transform resourceLocation, Transform parent, HexCell hexCell)
        {
            var newResource = Instantiate(this, resourceLocation.position, Quaternion.identity, parent);
            newResource.myHexCell = hexCell;
            hexCell.AddResource(newResource);
        }

        public float Gather(float gatherRate)
        {
            var gather = gatherAmount * gatherRate;
            if (currentResourceAmount >= gather)
                currentResourceAmount -= gather;
            else
            {
                gather = currentResourceAmount;
                currentResourceAmount = 0;
                onResourceEmpty(this);
                Remove();
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