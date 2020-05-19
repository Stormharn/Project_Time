using System.Collections;
using UnityEngine;
using ProjectTime.Build;
using ProjectTime.HexGrid;

namespace ProjectTime.Storm
{
    public class Storm : MonoBehaviour
    {
        [SerializeField] float stormDamage;
        [SerializeField] float damageTime;
        [SerializeField] float eddySpawnPercentChance;
        [SerializeField] Eddy eddyPrefab;
        [SerializeField] EddyWarning eddyWarningPrefab;
        [SerializeField] float newEddyTime;
        [SerializeField] float initialPeaceTime;
        [SerializeField] float eddyWarningTime;
        [SerializeField] float intensifyTime = 300f;
        [SerializeField] float intensifyDamage = 1.25f;
        [SerializeField] float intensifyDamageTime = .9f;
        [SerializeField] float intensifyStormParticles = 1.1f;
        [SerializeField] ParticleSystem[] particleSystems;
        int intensifyCount;
        WaitForSeconds stormDamageTimer;
        WaitForSeconds initialPeaceTimer;
        bool createdEddy = false;

        private void Start()
        {
            initialPeaceTimer = new WaitForSeconds(initialPeaceTime);
            stormDamageTimer = new WaitForSeconds(damageTime);
            StartCoroutine(nameof(TemporalStorm));
            StartCoroutine(nameof(TemporalEddy));
            StartCoroutine(nameof(StormIntensify));
        }

        private IEnumerator StormIntensify()
        {
            while (true)
            {
                yield return new WaitForSeconds(intensifyTime);
                stormDamage *= intensifyDamage;
                damageTime *= intensifyDamageTime;
                newEddyTime *= intensifyDamageTime;
                stormDamageTimer = new WaitForSeconds(damageTime);
                intensifyCount++;
                foreach (var particleSystem in particleSystems)
                {
                    var emissionModule = particleSystem.emission;
                    float curEmissionsMin = emissionModule.rateOverTime.constantMin;
                    float curEmissionsMax = emissionModule.rateOverTime.constantMax;
                    var minMax = emissionModule.rateOverTime;
                    minMax.constantMin = curEmissionsMin * intensifyStormParticles;
                    minMax.constantMax = curEmissionsMax * intensifyStormParticles;
                    emissionModule.rateOverTime = minMax;
                }
            }
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
                    StartCoroutine(nameof(NewEddy));
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

        private IEnumerator NewEddy()
        {
            var position = HexManager.Instance.RandomCell().transform.position;
            var direction = Instantiate(eddyWarningPrefab, position, Quaternion.identity, transform).InitializeWarning(eddyWarningTime, intensifyCount);
            yield return new WaitForSeconds(eddyWarningTime);
            Instantiate(eddyPrefab, position, Quaternion.identity, transform).InitializeEddy(direction, intensifyCount);
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