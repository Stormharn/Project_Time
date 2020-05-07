using System;
using System.Collections;
using System.Collections.Generic;
using ProjectTime.HexGrid;
using ProjectTime.Shielding;
using UnityEngine;

namespace ProjectTime.Build
{
    public class StartBase : ShieldGenerator
    {
        public override void Cleanup()
        {

        }

        private new void Start()
        {
            myCell = GameObject.FindObjectOfType<HexManager>().ClosestCell(transform.position);
            myCell.AddBuilding(this);
            myCell.PowerUp();
            base.Start();
        }
    }
}