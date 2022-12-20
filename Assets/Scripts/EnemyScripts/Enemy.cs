using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace OrkWizard
{
    public class Enemy : Character
    {
        [SerializeField]
        public EnemySO EnemySO;
        [SerializeField]
        public PlayerDetectorSO detectorSO;

        [SerializeField]
        private GameObject[] weaponsInitialObjects;


        public EnemyRigidBodyController RbController { get; private set; }
        public IEnemyMovement Movement { get; private set; }
        public AnimatorControllerBase Animator { get; private set; }
        public PlayerDetector PlayerDetector { get; private set; }
        public EnemyWeaponController WeaponController { get; private set; }
        public EnemyStateManager StateManager { get; private set; }
        public PlayerCharacter PlayerReference { get; private set; }
        public bool IsCriticallyHit { get;  set; }


        private BoxCollider2D collider;

        private float currentAttackCoolDownTime = 0;

        public const string playerTag = "Player";

        private void Awake()
        {
            collider = GetComponent<BoxCollider2D>();
            Animator = GetComponent<AnimatorControllerBase>();
            Movement = GetComponent<IEnemyMovement>();
            StateManager = GetComponent<EnemyStateManager>();
            RbController = new EnemyRigidBodyController(GetComponent<Rigidbody2D>(), this);
            PlayerDetector = new PlayerDetector(this, detectorSO);

            var weapons = new IWeapon[weaponsInitialObjects.Length];

            for (var i = 0; i < weaponsInitialObjects.Length; i++)
            {
                weapons[i] = weaponsInitialObjects[i].GetComponent<IWeapon>();
                weapons[i].ResetAtack();
            }
            weaponsInitialObjects = null;
            WeaponController = new EnemyWeaponController(this, weapons);
        }

        private void FixedUpdate()
        {
            if (currentAttackCoolDownTime > 0)
            {
                currentAttackCoolDownTime -= Time.deltaTime;
            }
        }

        public bool CanAttack()
        {
            if (currentAttackCoolDownTime <= 0)
            {
                return WeaponController.CanAttack();
            }

            return false;
        }

        public void StartAttackCoolDown()
        {
            currentAttackCoolDownTime = EnemySO.attackCoolDown + Random.Range(0, 0.3f);
        }

        public void SetPlayerReference(Collider2D collider)
        {
            PlayerReference = collider.gameObject.GetComponent<PlayerCharacter>();
        }

        public void SetPlayerReference(PlayerCharacter character)
        {
            PlayerReference = character;
        }

        public void ForgetPlayer()
        {
            PlayerReference = null;
        }

        public void SetMovement(bool enabled)
        {
            Movement.Enable(enabled);
        }

        public Vector2 GetColliderSize()
        {
            return collider.size;
        }

        public bool CollisionCheck(float distance, LayerMask layers)
        {
            var checkSide = IsFacingLeft ? Vector2.left : Vector2.right;
            return CollisionCheck(distance, layers, checkSide);
        }

        public bool CollisionCheck(float distance, LayerMask layers, Vector2 side)
        {
            var hit = CollisionCheckRayCast(side, transform.position, (collider.size.x / 2) + distance, layers);
            if (hit)
            {
                return true;
            }
            return false;
        }

        public void Flip(bool towardsPlayer)
        {
            if (PlayerReference != null && transform.position.x != PlayerReference.transform.position.x)
            {
                bool playerIsToTheLeft = transform.position.x >= PlayerReference.transform.position.x;

                if ((IsFacingLeft && towardsPlayer && !playerIsToTheLeft)
                    || IsFacingLeft && !towardsPlayer && playerIsToTheLeft
                    || !IsFacingLeft && towardsPlayer && playerIsToTheLeft
                    || !IsFacingLeft && !towardsPlayer && !playerIsToTheLeft)
                {
                    Flip();
                }
            }


        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.CompareTag(playerTag))
            {
                SetPlayerReference(collision.collider);
                StateManager.ChangeState(new GrappleState());
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.white;
            var direction = IsFacingLeft ? -1 : 1;
            Vector2 origin = (new Vector2(transform.position.x + detectorSO.offset.x * direction, transform.position.y + detectorSO.offset.y));

            Gizmos.DrawWireCube(origin, detectorSO.size);
            Gizmos.DrawWireSphere(origin, detectorSO.size.x);

        }
    }
}
