using UnityEngine;

namespace OrkWizard
{
    [CreateAssetMenu(fileName = "EnemyScriptableObject", menuName = "ScriptableObjects/EnemiesScriptableObjects/EnemyHealth")]
    class EnemyHealthSO : ScriptableObject
    {

        public float maxHp;
        public Element enemyElement;
        public Element weakElement;
    }
}