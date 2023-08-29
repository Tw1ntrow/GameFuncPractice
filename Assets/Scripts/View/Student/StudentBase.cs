using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectX.Battle.View.Unit
{
    public class StudentBase : UnitBase
    {
        [SerializeField]
        private Animator animator;

        private const string IdleTrigger = "Idle";
        private const string TurnTrigger = "Turn";
        private const string MoveTrigger = "Move";

        public void Turn()
        {
            animator.SetTrigger(TurnTrigger);
        }

        public void Move()
        {
            animator.SetTrigger(MoveTrigger);
        }

        public void Idle()
        {
            animator.SetTrigger(IdleTrigger);
        }

    }

}
