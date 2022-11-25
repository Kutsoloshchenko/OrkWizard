using UnityEngine;

namespace OrkWizard
{
    [CreateAssetMenu(fileName = "PlayerScriptableObject", menuName = "ScriptableObjects/PlayerScriptableObjects/Player")]
    public class PlayerSO : ScriptableObject
    {

        // Maybe we change this to multiple SO?

        [Header("Player Stats")]
        public float maxHealth = 100;
        public float currentHealth;

        [Header("Horizontal Movement")]
        public float maxSpeed = 50;
        public float originalMaxSpeed = 50;
        public float timeTillMaxSpeed = 3;
        public float walkSpeed = 10;
        public float manualSpeedMultiplier = 0.75f;
        public float powerSlideSpeedMultiplier = 1.5f;
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

        [Header("Weapons")]
        public float weaponOffset = 2f;

        [Header("Dmg receiver")]
        public float dmgKnockbackDuration = 0.5f;
        public float dmgKnockbackXForce = 10;
        public float dmgKnockbackYForce = 7;
    }
}
