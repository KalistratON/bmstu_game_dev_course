using LearnGame.Timer;

using System;

namespace LearnGame.Timer {

    public class TimerUIModel
    {
        public event Action TimerEnd;


        private readonly ITimer myTimer;


        public float TimerSeconds { get; private set; }


        public TimerUIModel ()
        {
            myTimer = new UnityTimer();
        }

        public int GetTime (float theDuration)
        {
            TimerSeconds += myTimer.DeltaTime;
            if (TimerSeconds >= theDuration)
            {
                TimerEnd?.Invoke();
            }
            return (int)(theDuration - TimerSeconds);
        }
    }
}