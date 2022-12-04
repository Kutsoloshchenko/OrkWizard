using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace OrkWizard
{
    public class PlayerRigidbodyController : BaseRigidbodyController
    {
        private PlayerCharacter character;

        public PlayerRigidbodyController(Rigidbody2D rb, PlayerCharacter characterObject) : base(rb)
        {
            character = characterObject;
        }

        public void ApplyKnockBackForce()
        {
            var direction = character.IsFacingLeft ? 1 : -1;
            Vector2 force = new Vector2(character.playerScriptableObject.dmgKnockbackXForce * direction, character.playerScriptableObject.dmgKnockbackYForce);
            ApplyForce(force);
        }

        public override void UpdateSpeed(float xSpeed, float ySpeed)
        {
            base.UpdateSpeed(xSpeed, ySpeed);
            character.UpdateDebugSpeed(rigidbody.velocity.x, rigidbody.velocity.y);
        }
    }
}
