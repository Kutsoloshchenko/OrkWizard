using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace OrkWizard
{
    public class FallingPlatfrom : PlatformHandler
    {
        private bool shouldMove;
        private Vector2 originalPossition;

        [SerializeField]
        protected FallingPlatformSO fallingSO;

        protected override void Initialize()
        {
            base.Initialize();
            originalPossition = transform.position;
        }

        public void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            if (shouldMove)
            {
                Fall();
            }
        }

        private void Fall()
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.gravityScale = 1;
            rb.velocity = new Vector2(0, -fallingSO.fallingSpeed);
            boxCollider.enabled = false;

            Invoke("Reset", fallingSO.platformResetTime);
        }

        private void StartFallingAfterTime()
        {
            shouldMove = true;
        }

        private void Reset()
        {
            shouldMove = false;
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.gravityScale = 0;
            rb.velocity = Vector2.zero;
            boxCollider.enabled = true;
            transform.position = originalPossition;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag(_playerTag))
            {
                Invoke("StartFallingAfterTime", fallingSO.fallDelay);
            }
        }
    }
}
