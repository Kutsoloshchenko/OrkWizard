using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace OrkWizard
{
    public class TrapActivator : MonoBehaviour
    {
        protected const string _playerTag = "Player";
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(_playerTag))
            {
                var trapManager = GetComponentInParent<TrapManager>();

                if (trapManager != null)
                {
                    trapManager.Activate();
                }
            }
        }
    }
}
