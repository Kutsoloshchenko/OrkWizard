using UnityEngine;

namespace OrkWizard
{
    public class BaseRigidbodyController
    {

        protected Rigidbody2D rigidbody;

        public BaseRigidbodyController(Rigidbody2D rb)
        {
            rigidbody = rb;
        }


        public virtual void SetGravity(float v)
        {
            rigidbody.gravityScale = v;
        }

        public virtual Vector2 GetCurrentSpeed()
        {
            if (rigidbody != null)
            {
                return rigidbody.velocity;
            }
            return Vector2.zero;
        }

        public virtual void UpdateSpeed(float xSpeed, float ySpeed)
        {
            rigidbody.velocity = new Vector2(xSpeed, ySpeed);
        }

        public virtual void UpdateXSpeed(float speed)
        {
            UpdateSpeed(speed, rigidbody.velocity.y);
        }

        public virtual void UpdateYSpeed(float speed)
        {
            UpdateSpeed(rigidbody.velocity.x, speed);
        }

        public virtual void ApplyForce(Vector2 force)
        {
            rigidbody.AddForce(force, ForceMode2D.Impulse);
        }


    }
}
