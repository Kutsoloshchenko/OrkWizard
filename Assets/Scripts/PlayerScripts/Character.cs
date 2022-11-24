using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


namespace OrkWizard
{
    public class Character : MonoBehaviour
    {
        public InputDetection Input { get; private set; }
        public HorizontalMovement HorizontalMovement { get; private set; }
        public Jump JumpMovement { get; private set; }
        public PlayerWeaponController WeaponController { get; private set; }
        public AnimatorController Animator { get; private set; }

        public bool IsPowerSliding { get; private set; }

        private Rigidbody2D rigidBody;
        private CapsuleCollider2D playerCollider;
        private GameObject currentPlatform;
        
        
        [SerializeField]
        private Text horizontalSpeed;
        [SerializeField]
        private Text verticalSpeed;
        [SerializeField]
        private Text weapon;

        [SerializeField]
        private LayerMask platformCollisionLayer;
        [SerializeField]
        private LayerMask wallCollistionLayer;

        [SerializeField]
        private float distanceToCollider;

        [HideInInspector]
        public bool isFacingLeft;

        [HideInInspector]
        public bool isJumping;

        [HideInInspector]
        public bool isGrounded;

        [HideInInspector]
        public bool isPlatformHanging;

        [SerializeField]
        public PlayerSO playerScriptableObject;

        void Awake()
        {
            Initialization();
        }

        protected virtual void Initialization()
        {
            rigidBody = GetComponent<Rigidbody2D>();
            playerCollider = GetComponent<CapsuleCollider2D>();
            Animator = GetComponent<AnimatorController>();
            Input = GetComponent<InputDetection>();
            HorizontalMovement = GetComponent<HorizontalMovement>();
            JumpMovement = GetComponent<Jump>();
            WeaponController = GetComponent<PlayerWeaponController>();
        }

        private void Update()
        {
            // obviously need to be in a class of its own, but its for debug 
            if (Input.RestartPressed())
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            Debug.DrawRay(transform.position, Vector2.down *((playerCollider.size.y / 2) + distanceToCollider), Color.green );
        }

        internal void SetGravity(float v)
        {
            rigidBody.gravityScale = v;
        }

        internal Vector2 GetColliderSize()
        {
            return playerCollider.size;
        }

        public virtual void Flip()
        {
            isFacingLeft = !isFacingLeft;
            transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        }

        public bool CheckGroundRayCast()
        {
            var hit = CollisionCheckRayCast(Vector2.down, transform.position, (playerCollider.size.y / 2) + distanceToCollider, platformCollisionLayer);

            if (hit)
            {
                currentPlatform = hit.collider.gameObject;
                transform.SetParent(currentPlatform.transform);
                return true;
            }
            currentPlatform = null;
            return false;
        }

        public bool WallCheck()
        {
            var checkSide = isFacingLeft ? Vector2.left : Vector2.right;
            var hit = CollisionCheckRayCast(checkSide, transform.position, (playerCollider.size.x / 2) + distanceToCollider, wallCollistionLayer);
            if (hit)
            {
                return true;
            }
            return false;
        }

        public Vector2 GetCurrentSpeed()
        {
            if (rigidBody != null)
            {
                return rigidBody.velocity;
            }
            return Vector2.zero;
        }

        public RaycastHit2D PlatformSideCheck()
        {
            var checkSide = isFacingLeft ? Vector2.left : Vector2.right;
            var origin = new Vector2(transform.position.x, transform.position.y + (playerCollider.size.y / 4));
            var hit = CollisionCheckRayCast(checkSide, origin, (playerCollider.size.x / 2) + distanceToCollider, platformCollisionLayer);
            return hit;
        }

        internal void UpdateDebugWeapon(string weaponName)
        {
            weapon.text = $"Weapon: {weaponName}";
        }

        public bool Falling(float velocity)
        {
            if (!isPlatformHanging && (!isGrounded && rigidBody.velocity.y < velocity))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool PowerSliding()
        {
            if (currentPlatform != null)
            {
                var angle = currentPlatform.GetComponentInParent<PlatformHandler>().GetPlatformAngle();
                // Currently we have all tillted platform to have 15 or -15 angle.
                if (Mathf.Abs(angle) == 15)
                {
                    if ((angle == 15 && !isFacingLeft) || (angle == -15 && isFacingLeft))
                    {
                        Flip();
                    }

                    transform.localEulerAngles = new Vector3(0, 0, angle);
                    IsPowerSliding = true;
                    return true;
                }
            }
            IsPowerSliding = false;
            transform.localEulerAngles = new Vector3(0, 0, 0);
            return false;
        }

        public void UpdateXSpeed(float speed)
        {
            rigidBody.velocity = new Vector2(speed, rigidBody.velocity.y);
            horizontalSpeed.text = $"Horizontal speed: {speed}";
        }

        public void UpdateYSpeed(float speed)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, speed);
            verticalSpeed.text = $"Vertical speed: {speed}";
        }

        public void CapHorizontalSpeed(float number)
        {
            playerScriptableObject.maxSpeed = number;
        }

        public void SetHorizontalMovement(bool enabled)
        {
            HorizontalMovement.enabled = enabled;
        }

        public void SetVerticalMovement(bool enabled)
        {
            JumpMovement.enabled = enabled;
        }

        public void SetWeaponControls(bool enabled)
        {
            WeaponController.enabled = enabled;
        }


        protected void FallSpeed(float speed)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, rigidBody.velocity.y * speed);
        }

        private RaycastHit2D CollisionCheckRayCast(Vector2 direction, Vector2 originPoint, float distance, LayerMask collision)
        {
            var hit = Physics2D.Raycast(originPoint, direction, distance, collision);
            return hit;
        }


    }

}
