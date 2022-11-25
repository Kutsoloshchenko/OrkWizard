using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace OrkWizard
{
    public class AnimatorControllerBase : MonoBehaviour
    {
        protected Animator animator;
        protected string currentAnimationName;

        protected void Awake()
        {
            Initialize();
        }

        protected virtual void Initialize()
        {
            animator = GetComponent<Animator>();
        }

        public void ChangeAnimation(string state)
        {
            if (currentAnimationName != state)
            {
                animator.Play(state);
                currentAnimationName = state;
            }
            
        }
    }
}
