using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace OrkWizard
{
    public abstract class AttackSuperState : IState
    {
        public abstract void OnEnter(BaseStateManager stateManager);
        public abstract void OnExit(BaseStateManager stateManager);
        public abstract void OnFixedUpdate(BaseStateManager stateManager);
        public abstract void OnUpdate(BaseStateManager stateManager);

        protected void HandleChangeState(BaseStateManager stateManager)
        {
            if (stateManager.Character.CheckGroundRayCast())
            {

                // I wanna see how it behaves;
                //if (stateManager.Character.GetCurrentSpeed().x != 0)
                //{
                //    stateManager.ChangeState(stateManager.MovingState);
                //    return;
                //}
                //else
                //{
                stateManager.ChangeState(new IdleState());
                //}
            }
            else
            {
                stateManager.ChangeState(new InAirState());
            }
        }

        protected Vector2 FindStartLocation(BaseStateManager stateManager, Vector2 direction)
        {
            var weaponOffsetX = stateManager.Character.WeaponController.CurrentWeapon.GetWeaponOffsetX();
            var weaponOffsetY = stateManager.Character.WeaponController.CurrentWeapon.GetWeaponOffsetY();

            var colliderSize = stateManager.Character.GetColliderSize();

            float x = stateManager.Character.transform.position.x + (colliderSize.x * 0.5f + weaponOffsetX) * direction.x;
            float y = stateManager.Character.transform.position.y + weaponOffsetY;

            return new Vector2(x, y);
        }
    }
}
