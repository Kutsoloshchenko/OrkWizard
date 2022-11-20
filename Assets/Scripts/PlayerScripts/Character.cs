using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


namespace OrkWizard
{
    public class Character : MonoBehaviour
    {
        private Rigidbody2D rigidBody;
        private CapsuleCollider2D playerCollider;
        private AnimatorController animator;
        private InputDetection input;
        private HorizontalMovement horizontalMovement;
        private Jump jumpMovement;
        private GameObject currentPlatform;
        private PlayerWeaponController weaponController;
        
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

        // Start is called before the first frame update
        void Start()
        {
            Initialization();
        }

        private void Update()
        {
            // obviously need to be in a class of its own, but its for debug 
            if (input.RestartPressed())
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            Debug.DrawRay(transform.position, Vector2.down *((playerCollider.size.y / 2) + distanceToCollider), Color.green );
        }

        protected virtual void Initialization()
        {
            rigidBody = GetComponent<Rigidbody2D>();
            playerCollider = GetComponent<CapsuleCollider2D>();
            animator = GetComponent<AnimatorController>();
            input = GetComponent<InputDetection>();
            horizontalMovement = GetComponent<HorizontalMovement>();
            jumpMovement = GetComponent<Jump>();
            weaponController = GetComponent<PlayerWeaponController>();
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
                animator.SetWallTouch(true);
                return true;
            }
            animator.SetWallTouch(false);
            return false;
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
                    weaponController.enabled = false;
                    return true;
                }
            }
            if (!weaponController.enabled)
            {
                weaponController.enabled = true;
            }
            
            transform.localEulerAngles = new Vector3(0, 0, 0);
            return false;
        }

        public void UpdateDebugSpeed(float speed, bool isVertical)
        {
            if (isVertical)
            {
                verticalSpeed.text = $"Vertical speed: {speed}";
            }
            else
            {
                horizontalSpeed.text = $"Horizontal speed: {speed}";
            }
        }

        public void CapHorizontalSpeed(float number)
        {
            playerScriptableObject.maxSpeed = number;
        }

        public void SetHorizontalMovement(bool enabled)
        {
            horizontalMovement.enabled = enabled;
        }

        public void SetVerticalMovement(bool enabled)
        {
            jumpMovement.enabled = enabled;
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
