using UnityEngine;

namespace OrkWizard
{
    [CreateAssetMenu(fileName = "PlatformScriptableObject", menuName = "ScriptableObjects/PlatformScriptableObjects/MovingPlatform")]
    public class MovingPlatformSO : ScriptableObject
    {
        public PlatformMovementType platformType;
        public float platformSpeed;
        public bool playerTriggerMovement;
        public float stopTime;
        public bool finishOneTime;
        public float platformResetTime;
    }
}
