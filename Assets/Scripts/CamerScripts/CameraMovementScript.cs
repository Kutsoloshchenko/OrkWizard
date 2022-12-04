using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OrkWizard

{
    public class CameraMovementScript : MonoBehaviour
    {
        private PlayerCharacter character;


        // Start is called before the first frame update
        void Start()
        {
            character = FindObjectOfType<PlayerCharacter>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            transform.position = new Vector3(character.transform.position.x, character.transform.position.y, -10);

        }
    }
}

