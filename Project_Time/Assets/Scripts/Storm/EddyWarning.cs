using System;
using System.Collections;
using System.Collections.Generic;
using ProjectTime.HexGrid;
using UnityEngine;

namespace ProjectTime.Storm
{
    public class EddyWarning : MonoBehaviour
    {
        [SerializeField] float movementSpeed = 1f;
        Transform myTranform;
        MovementDirections randomDirection;
        float lifetime;
        float timeAlive;

        private void Start()
        {
            GetComponentInChildren<MeshFilter>().mesh = GameObject.FindObjectOfType<HexMesh>().hexMesh;
            timeAlive = Mathf.Epsilon;
            myTranform = transform;
            Debug.Log("Warning.... Warning.... Incoming Eddy");
        }

        public MovementDirections InitializeWarning(float warningTime)
        {
            lifetime = warningTime;
            var values = Enum.GetValues(typeof(MovementDirections));
            randomDirection = (MovementDirections)values.GetValue(UnityEngine.Random.Range(0, values.Length));
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
    }
}