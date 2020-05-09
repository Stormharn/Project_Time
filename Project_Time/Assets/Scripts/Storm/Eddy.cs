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
            var values = Enum.GetValues(typeof(MovementDirections));
            randomDirection = (MovementDirections)values.GetValue(UnityEngine.Random.Range(0, values.Length));
            myTranform = transform;
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