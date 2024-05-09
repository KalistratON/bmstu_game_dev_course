using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public EnviromentController myEnviromentController;

    [Space(5)]
    public Button PlayerButton;
    public Button CubeButton;
    public Button SphereButton;

    [Space(5)]
    public Button ShowAllButton;
    public Image ShowAllImage;

    [Space(5)]
    public Slider mySlider;

    [Space(5)]
    public InputField myInputField;

    private void Start()
    {
        PlayerButton.onClick.AddListener(myEnviromentController.ShowPlayer);
        CubeButton.onClick.AddListener(myEnviromentController.ShowCube);
        SphereButton.onClick.AddListener(myEnviromentController.ShowSphere);

        ShowAllButton.interactable = false;
        ShowAllImage.color = Color.red;
        ShowAllButton.onClick.AddListener(myEnviromentController.ShowAllObjects);

        mySlider.onValueChanged.AddListener(ShowValueDebug);

        myInputField.onEndEdit.AddListener(ShowDataPlayerInput);
    }

    public void AllShowButtonState (bool value)
    {
        ShowAllButton.interactable = value;
        ShowAllImage.color = value ? Color.green : Color.red;
    }

    public void ShowValueDebug(float value)
    {
    }

    public void ShowDataPlayerInput(string value)
    {
    }
}
