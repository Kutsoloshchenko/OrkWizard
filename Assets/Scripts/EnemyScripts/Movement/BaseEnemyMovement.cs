using System;
using UnityEngine;

namespace OrkWizard
{
    public abstract class BaseEnemyMovement : MonoBehaviour
    {

        protected Enemy enemy;

        protected MovementType currentMovementType;
        protected Action moveFunction;

        [SerializeField]
        protected EnemyMovementSO movementSO;

        protected virtual void Awake()
        {
            enemy = GetComponent<Enemy>();
        }

        protected virtual void FixedUpdate()
        {
            moveFunction();
        }

        public virtual void Enable(bool value)
        {
            this.enabled = value;
        }

        public virtual void SetCurrentMovementType(MovementType type)
        {
            currentMovementType = type;
            AdjustMovementFunction();
        }

        protected virtual void AdjustMovementFunction()
        {
            switch (currentMovementType)
            {

                case MovementType.Patrol:
                    moveFunction = Patrol;
                    break;

                case MovementType.OffensiveManuvers:
                    moveFunction = OffensiveManuvers;
                    break;

                case MovementType.RunAway:
                    moveFunction = RunAway;
                    break;

                case MovementType.NoMovement:
                default:
                    moveFunction = () => { return; };
                    break;
            }
        }
        protected abstract void Patrol();
        protected abstract void OffensiveManuvers();
        protected abstract void RunAway();

    }
}
