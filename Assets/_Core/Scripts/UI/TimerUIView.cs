using LearnGame.Timer;

using UnityEngine;

using TMPro;

namespace LearnGame.UI 
    { 
public class TimerUIView : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI myOutputText;
    
    [field: SerializeField]
    public TimerUIConfig myConfig;

    private string myFormat;

    public TimerUIModel myTimerUIModel;

    public void Initialize (ITimer theTimer)
    {
        myFormat = myOutputText.text;
        myTimerUIModel = new TimerUIModel (theTimer);
    }

    private void Update()
    {
        int time = myTimerUIModel.GetTime (myConfig.GameDurationSeconds);
        myOutputText.text = string.Format (myConfig.Format, time / 60, time % 60);
    }
}
}
