using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectTime.HexGrid;

namespace ProjectTime.Resources
{
    public class ResourceManager : MonoBehaviour
    {
        // Declarations
        #region Declarations
        [SerializeField] Resource resource = null;
        List<Resource> resourceTypes = new List<Resource>();
        Transform resourceParent;
        #endregion

        // Unity Methods
        #region Unity Methods
        private void Awake()
        {
            resourceParent = GameObject.FindGameObjectWithTag(UnityTags.ResourceParent.ToString()).transform;
        }
        private void Start()
        {
            var hexCells = GameObject.FindGameObjectsWithTag(UnityTags.HexCell.ToString());
            foreach (var hexCell in hexCells)
            {
                if (Random.Range(1f, 100f) < 10)
                {
                    // resource = resourceTypes[Random.Range(0, resourceTypes.Count - 1)]; TODO add back random resource selection
                    var curHexCell = hexCell.GetComponent<HexCell>();
                    var newResource = Instantiate(resource, curHexCell.transform.position, Quaternion.identity, resourceParent);
                    curHexCell.AddResource(newResource);
                }
            }
        }

        #endregion

        // Public Methods
        #region Public Methods

        #endregion

        // Private Methods
        #region Private Methods

        #endregion
    }
}