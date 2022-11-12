using UnityEngine;

namespace OrkWizard
{
    [CreateAssetMenu(fileName = "PlatformScriptableObject", menuName = "ScriptableObjects/PlatformScriptableObjects/Platform")]
    public class PlatformScriptableObject : ScriptableObject
    {
        public PlatformType platformType;
        public float platformSpeed;
        public bool playerTriggerMovement;
        public float stopTime;
        public bool finishOneTime;
    }
}
