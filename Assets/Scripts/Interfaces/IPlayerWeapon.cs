using UnityEngine;

namespace OrkWizard
{
    public interface IPlayerWeapon
    {
        //void Attack();
        //void Attack(GameObject player);
        void Attack(Vector2 originalPossition, Vector2 direction, Vector2 initialSpeed);
        float GetAnimationLength();
        float GetMaxHorizontalSpeed();
        string GetAnimationName();
        void ResetAtack();
        bool CanAttack();
        void DisableNewAttacks();
        bool IsThroable();
        float GetWeaponOffsetY();
        float GetWeaponOffsetX();
    }
}
