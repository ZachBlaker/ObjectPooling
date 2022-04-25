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
            //Delayed a frame to avoid errors that occur 
            //when the disableAction moves the transform in the same frame the object is disabled
            Invoke("InvokeDisableAction" , 0);  
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
