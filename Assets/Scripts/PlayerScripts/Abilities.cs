using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OrkWizard
{
    public class Abilities : MonoBehaviour
    {
        protected Rigidbody2D rigidBody;
        protected Character character;
        protected CapsuleCollider2D playerCollider;
        protected AnimatorController animator;
        protected InputDetection input;

        protected virtual void Initialization()
        {
            character = GetComponent<Character>();
            rigidBody = GetComponent<Rigidbody2D>();
            playerCollider = GetComponent<CapsuleCollider2D>();
            animator = GetComponent<AnimatorController>();
            input = GetComponent<InputDetection>();
        }

        // Start is called before the first frame update
        void Start()
        {
            Initialization();
        }
    }
}
