using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class TimerUI : MonoBehaviour
{
    public event Action TimerEnd;

    [SerializeField]
    private TextMeshProUGUI myOutputText;
    private string myFormat;

    [field: SerializeField]
    public float GameDurationSeconds { get; private set; }

    public float TimerSeconds { get; private set; }

    private bool myTimerEnd;

    private void Start()
    {
        myFormat = myOutputText.text;
        myTimerEnd = false;
    }

    private void Update()
    {
        if (myTimerEnd)
        {
            return;
        }

        TimerSeconds += Time.deltaTime;
        if (TimerSeconds >= GameDurationSeconds)
        {
            TimerEnd?.Invoke();
            myTimerEnd = true;
        }
        int time = (int)(GameDurationSeconds - TimerSeconds);
        myOutputText.text = string.Format(myFormat, time / 60, time % 60);
    }
}
