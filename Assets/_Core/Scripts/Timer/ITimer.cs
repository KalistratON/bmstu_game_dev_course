namespace LearnGame.Timer
{
    public interface ITimer
    {
        float DeltaTime { get; }

        void SetTimeScale (float theTimeScale);
    }
}
