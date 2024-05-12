using UnityEngine;

namespace LearnGame.UI
{
    public class LosePanelView : MonoBehaviour
    {
        [SerializeField]
        private GameManager myGameManager;

        [SerializeField]
        private AudioSource myAudioSource;

        void Start()
        {
            myGameManager.Lose += ShowPanel;
            gameObject.SetActive (false);
        }

        private void ShowPanel()
        {
            gameObject.SetActive (true);
            myAudioSource.Play();
        }
    }
}