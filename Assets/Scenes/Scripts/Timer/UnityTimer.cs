using UnityEngine;

namespace LearnGame.Timer
{
    public class UnityTimer : ITimer
    {
        public float DeltaTime => Time.deltaTime;

        public void SetTimeScale (float theTimeScale)
        {
            Time.timeScale = theTimeScale;
        }
    }
}
