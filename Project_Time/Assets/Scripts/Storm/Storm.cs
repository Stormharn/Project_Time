using System;
using System.Collections;
using System.Collections.Generic;
using ProjectTime.Build;
using ProjectTime.HexGrid;
using UnityEngine;

namespace ProjectTime.Storm
{
    public class Storm : MonoBehaviour
    {
        [SerializeField] float stormDamage;
        [SerializeField] float damageTime;
        [SerializeField] float eddySpawnPercentChance;
        [SerializeField] Eddy eddyPrefab;
        [SerializeField] float newEddyTime;
        [SerializeField] float initialPeaceTime;
        WaitForSeconds stormDamageTimer;
        WaitForSeconds initialPeaceTimer;
        HexManager hexManager;
        bool createdEddy = false;

        private void Start()
        {
            hexManager = GameObject.FindObjectOfType<HexManager>();
            initialPeaceTimer = new WaitForSeconds(initialPeaceTime);
            stormDamageTimer = new WaitForSeconds(damageTime);
            StartCoroutine(nameof(TemporalStorm));
            StartCoroutine(nameof(TemporalEddy));
        }

        private IEnumerator TemporalEddy()
        {
            var loopsSinceLastEddy = 0;
            yield return initialPeaceTimer;
            while (true)
            {
                loopsSinceLastEddy++;
                createdEddy = false;
                if (UnityEngine.Random.Range(1f, 100f) < eddySpawnPercentChance)
                {
                    Instantiate(eddyPrefab, hexManager.RandomCell().transform.position, Quaternion.identity, transform);
                    createdEddy = true;
                }

                if (createdEddy)
                {
                    loopsSinceLastEddy = 0;
                    yield return new WaitForSeconds(newEddyTime);
                }
                else
                    yield return new WaitForSeconds(newEddyTime / (2 * loopsSinceLastEddy));
            }
        }

        private IEnumerator TemporalStorm()
        {
            while (true)
            {
                yield return stormDamageTimer;
                var buildings = GameObject.FindObjectsOfType<Building>();
                foreach (var building in buildings)
                {
                    if (building.IsShielded()) { continue; }
                    building.TakeDamage(stormDamage);
                }
            }
        }
    }
}