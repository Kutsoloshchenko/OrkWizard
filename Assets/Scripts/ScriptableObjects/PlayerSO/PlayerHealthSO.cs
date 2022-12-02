using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace OrkWizard
{
    [CreateAssetMenu(fileName = "PlayerScriptableObject", menuName = "ScriptableObjects/PlayerScriptableObjects/PlayerHealth")]
    public class PlayerHealthSO : ScriptableObject
    {
        [Header("Player Stats")]
        public float maxHealth = 100;
        public float currentHealth;

        public float invulDuration = 3;
    }
}
