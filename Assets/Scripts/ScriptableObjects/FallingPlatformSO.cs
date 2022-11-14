using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace OrkWizard
{
    [CreateAssetMenu(fileName = "PlatformScriptableObject", menuName = "ScriptableObjects/PlatformScriptableObjects/FallingPlatform")]
    public class FallingPlatformSO : ScriptableObject
    {
        public float fallDelay;
        public float platformResetTime;
        public float fallingSpeed;
    }
}
