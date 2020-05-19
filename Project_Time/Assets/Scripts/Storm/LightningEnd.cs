using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectTime.Storm
{
    public class LightningEnd : MonoBehaviour
    {
        private void Awake()
        {
            var variance = new Vector3(Random.Range(-8.66f, 8.66f) / 2, 0, Random.Range(-8.66f, 8.66f) / 2);
            transform.localPosition = variance;
        }
    }
}