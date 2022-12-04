using UnityEngine;

namespace OrkWizard
{
    [CreateAssetMenu(fileName = "ProjectileScriptableObject", menuName = "ScriptableObjects/Projectiles/Projectiles")]
    public class ProjectileSO : ScriptableObject
    {
        public float force;
        public float dmgValue;
        public Element damageType;
        public float impactDmg;
        public float explosionRadius;
        public string dmgTag;
        public float lifeTime;
        public float torque;
        public AnimationClip explosionAnimationClip;
        public float gravityScale;
    }
}
