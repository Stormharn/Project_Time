using System;
using System.Collections;
using System.Collections.Generic;
using ProjectTime.HexGrid;
using UnityEngine;

namespace ProjectTime.Build
{
    public class StartBase : Building
    {
        public override void Cleanup()
        {

        }

        private void Start()
        {
            var hexCell = GameObject.FindGameObjectWithTag(UnityTags.StartBase.ToString()).GetComponent<HexCell>();
            hexCell.AddBuilding(this);
        }
    }
}