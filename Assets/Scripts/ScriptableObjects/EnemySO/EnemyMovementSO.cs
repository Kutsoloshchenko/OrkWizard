using System.Collections.Generic;
using UnityEngine;

namespace OrkWizard
{
    [CreateAssetMenu(fileName = "EnemyMovementSO", menuName = "ScriptableObjects/EnemiesScriptableObjects/EnemyMovement")]
    public class EnemyMovementSO : ScriptableObject
    {
        public float maxSpeed = 50;
        public float collisionDistance = 4;
        public LayerMask collistionLayers;
    }
}

