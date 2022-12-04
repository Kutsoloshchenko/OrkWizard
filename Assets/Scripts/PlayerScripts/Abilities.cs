using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OrkWizard
{
    public class Abilities : MonoBehaviour
    {
        protected PlayerCharacter character;

        protected virtual void Initialization()
        {
            character = GetComponent<PlayerCharacter>();
        }

        // Start is called before the first frame update
        void Awake()
        {
            Initialization();
        }
    }
}
