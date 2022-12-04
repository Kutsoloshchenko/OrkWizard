using UnityEngine;

namespace OrkWizard
{
    [CreateAssetMenu(fileName = "PlayerDetectorSO", menuName = "ScriptableObjects/EnemiesScriptableObjects/PlayerDetector")]
    public class PlayerDetectorSO : ScriptableObject
    {
        public Vector2 size;
        public Vector2 offset;
        public float angle;
        public LayerMask detectionLayer;
        public ColliderType colliderType;
    }
}
