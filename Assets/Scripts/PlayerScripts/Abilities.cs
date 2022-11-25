using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OrkWizard
{
    public class Abilities : MonoBehaviour
    {
        protected Character character;

        protected virtual void Initialization()
        {
            character = GetComponent<Character>();
        }

        // Start is called before the first frame update
        void Awake()
        {
            Initialization();
        }
    }
}
