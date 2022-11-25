using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace OrkWizard
{
    public class RigidbodyController
    {
        private Rigidbody2D rigidbody;
        private Character character;

        public RigidbodyController(Rigidbody2D rb, Character characterObject)
        {
            rigidbody = rb;
            character = characterObject;
        }

        internal void SetGravity(float v)
        {
            rigidbody.gravityScale = v;
        }

        public Vector2 GetCurrentSpeed()
        {
            if (rigidbody != null)
            {
                return rigidbody.velocity;
            }
            return Vector2.zero;
        }

        public void UpdateSpeed(float xSpeed, float ySpeed)
        {
            rigidbody.velocity = new Vector2(xSpeed, ySpeed);
            character.UpdateDebugSpeed(rigidbody.velocity.x, rigidbody.velocity.y);
        }

        public void UpdateXSpeed(float speed)
        {
            UpdateSpeed(speed, rigidbody.velocity.y);
        }

        public void UpdateYSpeed(float speed)
        {
            UpdateSpeed(rigidbody.velocity.x, speed);
        }

        internal void ApplyForce(Vector2 force)
        {
            rigidbody.AddForce(force, ForceMode2D.Impulse);
        }
    }
}
