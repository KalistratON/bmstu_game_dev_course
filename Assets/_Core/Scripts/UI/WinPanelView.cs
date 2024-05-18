using LearnGame.Timer;

using UnityEngine;

namespace LearnGame.UI
{
    public class WinPanelView : MonoBehaviour
    {
        [SerializeField]
        private GameManager myGameManager;

        [SerializeField]
        private AudioSource myAudioSource;

        void Start()
        {
            myGameManager.Win += ShowPanel;
            gameObject.SetActive (false);
        }

        private void ShowPanel (ITimer theTimer)
        {
            gameObject.SetActive (true);
            myAudioSource.Play();
            theTimer.SetTimeScale (0.0f);
        }
    }
}