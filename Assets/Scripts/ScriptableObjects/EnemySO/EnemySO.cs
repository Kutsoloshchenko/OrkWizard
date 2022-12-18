using UnityEngine;

namespace OrkWizard
{
    [CreateAssetMenu(fileName = "EnemyScriptableObject", menuName = "ScriptableObjects/EnemiesScriptableObjects/Enemy")]
    public class EnemySO : ScriptableObject
    {
        public float attackCoolDown;

        public Vector2 knockBackForce;
        public float knockBackTime;
        public AnimationClip knockBackClip;

        public AnimationClip deathAnimation;

        public AnimationClip grappleClip;
        
        public float grappleDmg;
    }
}
