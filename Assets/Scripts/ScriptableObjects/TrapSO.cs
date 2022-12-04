using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace OrkWizard
{
    [CreateAssetMenu(fileName = "TrapScriptableObject", menuName = "ScriptableObjects/TrapObjects/Trap")]
    public class TrapSO : ScriptableObject
    {
        public Element dmgType;
        public float damage;
        public bool trigeredByPlayer;
        public bool dealDmgOnlyToPlayer;
        public float dmgCooldown;
        public float downTime;
        public float activeTime;
        public int ticksAfterStop;
        public bool dmgOverTime;
    }
}
