using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


namespace OrkWizard
{
    public class PlatformHandler : MonoBehaviour, IHange
    {
        protected BoxCollider2D boxCollider;
        protected Rigidbody2D rb;
        protected const string _playerTag = "Player";
        protected float[] verticalAngles = new float[] { 0, 90, 180, 270, 360 };

        private void Start()
        {
            Initialize();
        }

        protected virtual void Initialize()
        {
            boxCollider = GetComponent<BoxCollider2D>();
            rb = GetComponent<Rigidbody2D>();
        }

        public float GetPlatformAngle()
        {
            var angle = transform.localEulerAngles.z;
            if (!verticalAngles.Contains(transform.localEulerAngles.z))
            {
                var platformAngle = (angle > 0 && angle < 90) || (angle > 180 && angle < 270) ? 15 : -15;
                return platformAngle;
            }
            return angle;
        }

        public Vector2 GetStandUpPossition(bool isFacingLeft)
        {
            var sign = isFacingLeft ? 1 : -1;

            var possition = new Vector2(transform.position.x + (sign * boxCollider.size.x / 2), transform.position.y + (boxCollider.size.y / 2));
            return possition;

        }

        public Vector2 GetHangPossition(bool isFacingLeft)
        {
            var sign = isFacingLeft ? 1 : -1;

            var possition = new Vector2(transform.position.x + (sign * boxCollider.size.x / 2), transform.position.y);
            return possition;

        }
    }
}