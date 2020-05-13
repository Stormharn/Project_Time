using System;
using ProjectTime.Shielding;
using UnityEngine;

namespace ProjectTime.Storm
{
    public class Eddy : MonoBehaviour
    {
        [SerializeField] float movementSpeed = 1f;
        [SerializeField] float damage;
        Transform myTranform;
        MovementDirections randomDirection;

        private void Start()
        {
            myTranform = transform;
        }

        public void InitializeEddy(MovementDirections direction)
        {
            randomDirection = direction;
        }

        private void Update()
        {
            myTranform.Translate(StormMovementDirections.Instance.GetDirections().GetVector(randomDirection) * movementSpeed * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == UnityTags.Shield.ToString())
                other.GetComponent<Shield>().TakeDamage(damage);
            else if (other.tag == UnityTags.KillBox.ToString())
                Destroy(gameObject);
        }
    }
}