using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LearnGame
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField]
        private BaseCharacter myCharacter;

        private Image myHealthBar;
        private float myMaxHealth;

        private void Start()
        {
            if (!myCharacter)
            {
                return;
            }
            myMaxHealth = myCharacter.myHealth;
            myHealthBar = GetComponent<Image>();
            myHealthBar.fillAmount = 1.0f;
        }

        private void Update()
        {
            if (!myCharacter)
            {
                return;
            }
            myHealthBar.fillAmount = myCharacter.myHealth / myMaxHealth;
            myHealthBar.color = Color.green * myHealthBar.fillAmount + Color.red * (1.0f - myHealthBar.fillAmount);
            Debug.Log(myHealthBar.fillAmount);
        }
    }
}