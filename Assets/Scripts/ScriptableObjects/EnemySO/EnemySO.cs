using UnityEngine;

namespace OrkWizard
{
    [CreateAssetMenu(fileName = "EnemyScriptableObject", menuName = "ScriptableObjects/EnemiesScriptableObjects/Enemy")]
    public class EnemySO : ScriptableObject
    {
        public float maxHp;
        public float attackCoolDown;

        public Element element;

        public float detectRange;
    }
}
