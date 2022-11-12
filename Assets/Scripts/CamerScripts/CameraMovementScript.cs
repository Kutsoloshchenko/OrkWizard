using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OrkWizard

{
    public class CameraMovementScript : MonoBehaviour
    {
        private Character character;


        // Start is called before the first frame update
        void Start()
        {
            character = FindObjectOfType<Character>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            transform.position = new Vector3(character.transform.position.x, character.transform.position.y, -10);

        }
    }
}

