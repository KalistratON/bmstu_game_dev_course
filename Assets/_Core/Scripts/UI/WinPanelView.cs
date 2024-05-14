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

        private void ShowPanel()
        {
            gameObject.SetActive (true);
            myAudioSource.Play();
        }
    }
}