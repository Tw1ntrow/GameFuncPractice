using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectX.Battle.View.Unit
{
    public class StudentBase : UnitBase
    {
        [SerializeField]
        private Animator animator;
        
        private const string moveTrigger = "Move";
        private const string idleTrigger = "Idle";
        public void Move()
        {
            animator.SetFloat("Speed", 1f);
        }

        public void Idle()
        {
            animator.SetTrigger(idleTrigger);
        }

        public void SetSpeed(float speed)
        {
            animator.SetFloat("Speed", speed);
        }

    }

}
