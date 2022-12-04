using UnityEngine;

namespace OrkWizard
{
    [CreateAssetMenu(fileName = "PlayerWeaponScriptableObject", menuName = "ScriptableObjects/PlayerScriptableObjects/PlayerWeapon")]
    public class WeaponSO : ScriptableObject
    {
        public float speedAllowed;
        public float coolDownTime;
        public AnimationClip animationClip;
        public GameObject projectileObject;
        public bool throwable;
        public float offsetX;
        public float offsetY;
    }
}
