using UnityEngine;

namespace OrkWizard
{
    [CreateAssetMenu(fileName = "PlayerScriptableObject", menuName = "ScriptableObjects/PlayerScriptableObjects/Player")]
    public class PlayerSO : ScriptableObject
    {

        // How do i make it serializeble but still a private set?

        [Header("Horizontal Movement")]
        public float maxSpeed = 50;
        public float originalMaxSpeed = 50;
        public float timeTillMaxSpeed = 3;
        public float walkSpeed = 10;
        public float manualSpeedMultiplier = 0.75f;
        public float dragMultiplier = 10;

        [Header("Vertical Movement")]
        public float buttonHoldTime = 0.5f;
        public float maxJumpSpeed = 60;
        public float initialJumpSpeed = 20;
        public float maxFallSpeed = -35;
        public float scatingSpeed = 10;
        public float timeTillJumpSpeed = 0.5f;
        public float timeToJump = 0.1f;
        public float wallJumpTime = 0.5f;

        [Header("PlatformHang")]
        public float platformHangTimeOut = 0.3f;
        public float platformClimbTime = 0.5f;

        public float weaponOffset = 2f;
    }
}
