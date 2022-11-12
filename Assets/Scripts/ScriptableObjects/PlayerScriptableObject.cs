using UnityEngine;

namespace OrkWizard
{
    [CreateAssetMenu(fileName = "PlayerScriptableObject", menuName = "ScriptableObjects/PlayerScriptableObjects/Player")]
    public class PlayerScriptableObject : ScriptableObject
    {

        // How do i make it serializeble but still a private set?

        // Horizontal movement variables
        public float maxSpeed { get; private set; } = 50 ;
        public float timeTillMaxSpeed { get; private set; } = 3;
        public float walkSpeed { get; private set; } = 10;
        public float manualSpeedMultiplier { get; private set; } = 0.75f;
        public float dragMultiplier { get; private set; } = 10;


        // Vertical movement variables 
        public float buttonHoldTime { get; private set; } = 0.5f;
        public float maxJumpSpeed { get; private set; } = 60;
        public float initialJumpSpeed { get; private set; } = 20;
        public float maxFallSpeed { get; private set; } = -35;
        public float scatingSpeed { get; private set; } = 10;
        public float timeTillJumpSpeed { get; private set; } = 0.5f;
        public float timeToJump { get; private set; } = 0.1f;
        public float wallJumpTime { get; private set; } = 0.5f;

        // Platform hang variables
        public float platformHangTimeOut { get; private set; } = 0.3f;
        public float platformClimbTime { get; private set; } = 0.5f;
    }
}
