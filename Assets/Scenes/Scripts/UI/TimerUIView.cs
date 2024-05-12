using LearnGame.Timer;

using UnityEngine;

using TMPro;

public class TimerUIView : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI myOutputText;

    [field: SerializeField]
    public float GameDurationSeconds { get; private set; }

    private string myFormat;
    public TimerUIModel myTimerUIModel;


    private void Awake()
    {
        myFormat = myOutputText.text;
        myTimerUIModel = new TimerUIModel();
    }

    private void Update()
    {
        int time = myTimerUIModel.GetTime (GameDurationSeconds);
        myOutputText.text = string.Format (myFormat, time / 60, time % 60);
    }
}
