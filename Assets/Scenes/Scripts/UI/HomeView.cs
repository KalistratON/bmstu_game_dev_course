using LearnGame.Timer;

using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace LearnGame.UI
{
    public class HomeView : MonoBehaviour
    {
        private Button myHomeButton;

        private void Awake()
        {
            myHomeButton = GetComponent<Button>();

            myHomeButton.onClick.RemoveAllListeners();
            myHomeButton.onClick.AddListener (OpenMainLevel);
        }

        private void OpenMainLevel()
        {
            (new UnityTimer()).SetTimeScale (1.0f);

            SceneManager.LoadScene (0);
        }
    }
}