using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


namespace OrkWizard
{
    public class PlayerCharacter : Character
    {
        public InputDetection Input { get; private set; }
        public PlayerWeaponController WeaponController { get; private set; }
        public AnimatorController Animator { get; private set; }
        public PlayerRigidbodyController rbController { get; private set; }

        public bool IsPowerSliding { get; private set; }
        public bool RecivedOneTimeDmg { get; private set; }

        private CapsuleCollider2D playerCollider;
        private GameObject currentPlatform;
        private Jump jumpMovement;
        private HorizontalMovement horizontalMovement;

        [SerializeField]
        private Text horizontalSpeed;

        [SerializeField]
        private Text verticalSpeed;
        [SerializeField]
        private Text weapon;
        [SerializeField]
        private Text hp;

        [SerializeField]
        private LayerMask platformCollisionLayer;
        [SerializeField]
        private LayerMask wallCollistionLayer;

        [SerializeField]
        private float distanceToCollider;

        [HideInInspector]
        public bool isGrounded;

        [SerializeField]
        public PlayerSO playerScriptableObject;

        private void Awake()
        {
            Initialization();
        }

        protected virtual void Initialization()
        {
            rbController = new PlayerRigidbodyController(GetComponent<Rigidbody2D>(), this);
            playerCollider = GetComponent<CapsuleCollider2D>();
            Animator = GetComponent<AnimatorController>();
            Input = GetComponent<InputDetection>();
            jumpMovement = GetComponent<Jump>();
            WeaponController = GetComponent<PlayerWeaponController>();
            horizontalMovement = GetComponent<HorizontalMovement>();
        }

        private void Update()
        {
            // obviously need to be in a class of its own, but its for debug 
            if (Input.RestartPressed())
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            Debug.DrawRay(transform.position, Vector2.down * ((playerCollider.size.y / 2) + distanceToCollider), Color.green);
        }

        public Vector2 GetColliderSize()
        {
            return playerCollider.size;
        }

        public void SetRecivedDmg(bool value)
        {
            RecivedOneTimeDmg = value;
        }


        #region Debug information update
        public void UpdateDebugWeapon(string weaponName)
        {
            weapon.text = $"Weapon: {weaponName}";
        }

        public void UpdateHp(float number)
        {
            hp.text = $"HP: {number}";
        }

        public void UpdateDebugSpeed(float x, float y)
        {
            horizontalSpeed.text = $"Horizontal speed: {x}";
            verticalSpeed.text = $"Vertical speed: {y}";
        }
        #endregion

        #region Scripts enables 
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

        public void SetWeaponControls(bool enabled)
        {
            WeaponController.enabled = enabled;
        }

        public void ChangePlayerActiveStatus(bool active)
        {
            gameObject.SetActive(active);
        }
        #endregion

        #region Collider checks

        public bool PowerSliding()
        {
            if (currentPlatform != null)
            {
                var angle = currentPlatform.GetComponentInParent<PlatformHandler>().GetPlatformAngle();
                // Currently we have all tillted platform to have 15 or -15 angle.
                if (Mathf.Abs(angle) == 15)
                {
                    if ((angle == 15 && !IsFacingLeft) || (angle == -15 && IsFacingLeft))
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
            var checkSide = IsFacingLeft ? Vector2.left : Vector2.right;
            var hit = CollisionCheckRayCast(checkSide, transform.position, (playerCollider.size.x / 2) + distanceToCollider, wallCollistionLayer);
            if (hit)
            {
                return true;
            }
            return false;
        }

        public RaycastHit2D PlatformSideCheck()
        {
            var checkSide = IsFacingLeft ? Vector2.left : Vector2.right;
            var origin = new Vector2(transform.position.x, transform.position.y + (playerCollider.size.y / 4));
            var hit = CollisionCheckRayCast(checkSide, origin, (playerCollider.size.x / 2) + distanceToCollider, platformCollisionLayer);
            return hit;
        }

        #endregion
    }

}
