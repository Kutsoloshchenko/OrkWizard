using System;
using UnityEngine;

namespace OrkWizard
{
    public abstract class Character : MonoBehaviour
    {

        [HideInInspector]
        public bool IsFacingLeft { get; private set; }

        public virtual void Flip()
        {
            IsFacingLeft = !IsFacingLeft;
            transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        }


        protected RaycastHit2D CollisionCheckRayCast(Vector2 direction, Vector2 originPoint, float distance, LayerMask collision)
        {
            var hit = Physics2D.Raycast(originPoint, direction, distance, collision);
            return hit;
        }

    }
}
