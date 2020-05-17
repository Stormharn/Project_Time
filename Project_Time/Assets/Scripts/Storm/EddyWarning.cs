using System;
using UnityEngine;
using ProjectTime.HexGrid;

namespace ProjectTime.Storm
{
    public class EddyWarning : MonoBehaviour
    {
        [SerializeField] float movementSpeed = 1f;
        [SerializeField] float intensifyScale = .1f;
        Transform myTranform;
        MovementDirections randomDirection;
        float lifetime;
        float timeAlive;

        private void Start()
        {
            GetComponentInChildren<MeshFilter>().mesh = GameObject.FindObjectOfType<HexMesh>().hexMesh;
            timeAlive = Mathf.Epsilon;
        }

        public MovementDirections InitializeWarning(float warningTime, int count)
        {
            myTranform = transform;
            lifetime = warningTime;
            var values = Enum.GetValues(typeof(MovementDirections));
            randomDirection = (MovementDirections)values.GetValue(UnityEngine.Random.Range(0, values.Length));
            Intensify(count);
            return randomDirection;
        }

        private void Update()
        {
            timeAlive += Time.deltaTime;
            if (timeAlive < lifetime / 5f) { return; }
            myTranform.Translate(StormMovementDirections.Instance.GetDirections().GetVector(randomDirection) * movementSpeed * Time.deltaTime);
            if (timeAlive > lifetime)
                Destroy(gameObject);
        }

        private void Intensify(int count)
        {
            var newScale = myTranform.localScale;
            newScale.x = newScale.x + (count * intensifyScale / 2);
            newScale.z = newScale.z + (count * intensifyScale / 2);

            transform.localScale = newScale;
        }
    }
}