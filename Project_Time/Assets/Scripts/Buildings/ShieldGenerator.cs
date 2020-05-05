using UnityEngine;
using ProjectTime.HexGrid;
using ProjectTime.Shielding;
using System.Collections.Generic;

namespace ProjectTime.Build
{
    public class ShieldGenerator : Building
    {
        [SerializeField] Shield shield;
        int shieldLevel = 1;
        List<HexCell> cellsInRange = new List<HexCell>();

        public void Start()
        {
            var hexManager = GameObject.FindGameObjectWithTag(UnityTags.HexManager.ToString()).GetComponent<HexManager>();
            var shieldRange = (Hex.innerRadius * 2 * shieldLevel) + 1f;
            cellsInRange = hexManager.NearestCells(transform.position, shieldRange);

            foreach (var cell in cellsInRange)
            {
                shield.Spawn(cell.transform, transform, cell);
            }

        }
    }
}