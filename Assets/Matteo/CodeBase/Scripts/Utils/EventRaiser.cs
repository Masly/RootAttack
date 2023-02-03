using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CodeBase.Utils
{
    public class EventRaiser : MonoBehaviour
    {
        public bool callEventOnStart = false;
        public GameEvent eventToRaise;
        public float delayOnStart = 0;


        void Start()
        {
            if (callEventOnStart)
                Invoke("RaiseImmediate", delayOnStart);
        }
        public void RaiseEvent(float delay)
        {
            Invoke("RaiseImmediate", delay);
        }

        private void RaiseImmediate()
        {
            eventToRaise.Raise();
        }


    }
}