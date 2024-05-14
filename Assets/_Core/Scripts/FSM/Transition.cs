using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnGame.FSM
{
    public class Transition
    {
        public BaseState ToState { get; }
        public Func<bool> Condition { get; }
        public Transition(BaseState toState, Func<bool> condition)
        {
            ToState = toState;
            Condition = condition;
        }
    }
}
