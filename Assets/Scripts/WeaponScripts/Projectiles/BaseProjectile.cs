using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace OrkWizard
{
    public class BaseProjectile : AnimatorControllerBase
    {
        protected const string _allTag = "All";

        protected const string _idlePostfix = "_idle";
        protected const string _explosionPostfix = "_explosion";

        [SerializeField]
        protected ProjectileSO projectileSO;

        protected override void Initialize()
        {
            base.Initialize();
            ChangeAnimation(projectileSO.name + _idlePostfix);
        }
    }
}
