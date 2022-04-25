using UnityEngine;
using System;

namespace Gameplay.ObjectPooling
{
    public class PooledObject : MonoBehaviour
    {
        Action disableAction;
        Action destroyAction;

        public void SetDisableAction(Action disableAction)
        {
            this.disableAction = disableAction;
        }

        public void SetDestroyAction(Action destroyAction)
        {
            this.destroyAction = destroyAction;
        }

        void OnDisable()
        {
            Invoke("InvokeDisableAction",0);
        }
        void OnDestroy()
        {
            destroyAction?.Invoke();
        }

        private void InvokeDisableAction()
        {
            disableAction?.Invoke();
        }
    }
}
