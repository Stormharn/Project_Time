using UnityEngine;
using ProjectTime.Shielding;

namespace ProjectTime.Storm
{
    public class Eddy : MonoBehaviour
    {
        [SerializeField] float movementSpeed = 1f;
        [SerializeField] float damage;
        [SerializeField] float intensifyScale = .1f;
        [SerializeField] float intensifyDamage = 1.25f;
        Transform myTranform;
        MovementDirections randomDirection;

        public void InitializeEddy(MovementDirections direction, int count)
        {
            myTranform = transform;
            randomDirection = direction;
            Intensify(count);
        }

        private void Update()
        {
            myTranform.Translate(StormMovementDirections.Instance.GetDirections().GetVector(randomDirection) * movementSpeed * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == UnityTags.Shield.ToString())
            {
                other.GetComponent<Shield>().TakeDamage(damage);
                damage *= .5f;
            }
            else if (other.tag == UnityTags.KillBox.ToString())
                Destroy(gameObject);
        }

        public void Intensify(int count)
        {
            var newScale = myTranform.localScale;
            newScale.x = newScale.x + (count * intensifyScale);
            newScale.z = newScale.z + (count * intensifyScale);

            damage *= intensifyDamage * count;
            transform.localScale = newScale;
        }
    }
}