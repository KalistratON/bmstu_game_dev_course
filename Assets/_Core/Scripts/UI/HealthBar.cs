using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LearnGame
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField]
        private BaseCharacterView myCharacter;

        private Image myHealthBar;
        private float myMaxHealth;

        private void Start()
        {
            myHealthBar = GetComponent<Image>();

            if (myCharacter == null || myCharacter.Model == null)
            {
                myHealthBar.fillAmount = 0.0f;
                return;
            }
            myMaxHealth = myCharacter.Model.Health;
            myHealthBar.fillAmount = 1.0f;
        }

        private void Update()
        {
            if (myCharacter == null || myCharacter.Model == null)
            {
                myHealthBar.fillAmount = 0.0f;
                return;
            }

            myHealthBar.fillAmount = myCharacter.Model.Health / myMaxHealth;
            myHealthBar.color = Color.green * myHealthBar.fillAmount + Color.red * (1.0f - myHealthBar.fillAmount);
        }
    }
}