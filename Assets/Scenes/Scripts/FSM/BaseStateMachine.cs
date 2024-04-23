using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearnGame.Exceptions;

namespace LearnGame.FSM
{
    public class BaseStateMachine
    {
        private BaseState myCurrentState;
        private List<BaseState> myStates;
        private Dictionary<BaseState, List<Transition>> myTransition;

        public BaseStateMachine()
        {
            myStates = new List<BaseState>();
            myTransition = new Dictionary<BaseState, List<Transition>>();
        }

        public void SetInitialState(BaseState state)
        {
            myCurrentState = state;
        }

        public void AddState(BaseState state, List<Transition> transitions)
        {
            if (!myStates.Contains(state))
            {
                myStates.Add(state);
                myTransition.Add(state, transitions);
            }
            else
            {
                throw new AlreadyExistsException($"state {state.GetType()} already exists in state machine!");
            }
        }

        public void Update()
        {
            foreach (var transition in myTransition[myCurrentState])
            {
                if (transition.Condition())
                {
                    myCurrentState = transition.ToState;
                    break;
                }
            }

            myCurrentState.Execute();
        }
    }
}
