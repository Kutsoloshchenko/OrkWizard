using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


namespace OrkWizard
{
    public class Character : MonoBehaviour, IDamagable
    {
        public InputDetection Input { get; private set; }
        public PlayerWeaponController WeaponController { get; private set; }
        public AnimatorController Animator { get; private set; }
        public RigidbodyController rbController { get; private set; }


        public bool IsPowerSliding { get; private set; }

        private CapsuleCollider2D playerCollider;
        private GameObject currentPlatform;
        private Jump jumpMovement;
        private HorizontalMovement horizontalMovement;
        private StateManager stateManager;

        private bool applyDmgFromTick = false;

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
        public bool IsFacingLeft { get; private set; }

        [HideInInspector]
        public bool isGrounded;

        [HideInInspector]
        public bool RecivedOneTimeDmg { get; set; }

        [SerializeField]
        public PlayerSO playerScriptableObject;

        private void Awake()
        {
            Initialization();
        }

        protected virtual void Initialization()
        {

            playerScriptableObject.currentHealth = playerScriptableObject.maxHealth;

            rbController = new RigidbodyController(GetComponent<Rigidbody2D>(), this);
            playerCollider = GetComponent<CapsuleCollider2D>();
            Animator = GetComponent<AnimatorController>();
            Input = GetComponent<InputDetection>();
            jumpMovement = GetComponent<Jump>();
            WeaponController = GetComponent<PlayerWeaponController>();
            horizontalMovement = GetComponent<HorizontalMovement>();
            stateManager = GetComponent<StateManager>();
        }

        private void Update()
        {
            // obviously need to be in a class of its own, but its for debug 
            if (Input.RestartPressed())
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            Debug.DrawRay(transform.position, Vector2.down * ((playerCollider.size.y / 2) + distanceToCollider), Color.green);

            UpdateHp(playerScriptableObject.currentHealth);
        }

        internal Vector2 GetColliderSize()
        {
            return playerCollider.size;
        }

        public virtual void Flip()
        {
            IsFacingLeft = !IsFacingLeft;
            transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        }


        #region Debug information update
        internal void UpdateDebugWeapon(string weaponName)
        {
            weapon.text = $"Weapon: {weaponName}";
        }

        internal void UpdateHp(float number)
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

        private RaycastHit2D CollisionCheckRayCast(Vector2 direction, Vector2 originPoint, float distance, LayerMask collision)
        {
            var hit = Physics2D.Raycast(originPoint, direction, distance, collision);
            return hit;
        }

        #endregion

        #region IDamagable implementation


        public void ApplyDmg(float dmg)
        {
            ApplyDmg(dmg, DamageType.Physical);
        }

        public void ApplyDmg(float dmg, DamageType type)
        {
            // Apply dmg
            if (!RecivedOneTimeDmg)
            {
                rbController.UpdateSpeed(0, 0);
                stateManager.ChangeState(stateManager.DmgKnockbackState);
                var direction = IsFacingLeft ? 1 : -1;

                Vector2 force = new Vector2(playerScriptableObject.dmgKnockbackXForce*direction, playerScriptableObject.dmgKnockbackYForce);

                rbController.ApplyForce(force);

                SubtractPlayerHealth(dmg);
            }
        }

        public void ApplyDmg(float dmg, DamageType type, float tickTime)
        {
            // apply tick dmg;
            if (!applyDmgFromTick)
            {
                StartCoroutine(TickDmgCorutine(dmg, type, tickTime));
            }
        }

        public void StopTickDmg()
        {
            applyDmgFromTick = false;
        }

        public IEnumerator TickDmgCorutine(float dmg, DamageType type, float tickTime)
        {
            applyDmgFromTick = true;
            while (applyDmgFromTick)
            {
                SubtractPlayerHealth(dmg);
                yield return new WaitForSeconds(tickTime);
            }
        }

        public void SubtractPlayerHealth(float number)
        {
            playerScriptableObject.currentHealth -= number;

            if (playerScriptableObject.currentHealth <= 0)
            {
                stateManager.ChangeState(stateManager.DyingState);
            }
        }

        #endregion
    }

}
