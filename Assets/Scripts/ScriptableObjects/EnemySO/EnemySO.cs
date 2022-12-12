using UnityEngine;

namespace OrkWizard
{
    [CreateAssetMenu(fileName = "EnemyScriptableObject", menuName = "ScriptableObjects/EnemiesScriptableObjects/Enemy")]
    public class EnemySO : ScriptableObject
    {
        public float attackCoolDown;
        public float detectRange;

        public AnimationClip grappleClip;
        public float grappleDmg;
    }
}
