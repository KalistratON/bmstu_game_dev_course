using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviromentController : MonoBehaviour
{
    public UIController myUIController;
    [Space(5)]
    public GameObject Cube;
    public GameObject Sphere;
    public GameObject Player;

    public void ShowCube()
    {
        Cube.SetActive(!Cube.activeInHierarchy);

        ActiveAllShowButton();
    }

    public void ShowSphere()
    {
        Sphere.SetActive(!Sphere.activeInHierarchy);

        ActiveAllShowButton();
    }

    public void ShowPlayer()
    {
        Player.SetActive(!Player.activeInHierarchy);

        ActiveAllShowButton();
    }

    public void ShowAllObjects()
    {
        Cube.SetActive(true);
        Sphere.SetActive(true);
        Player.SetActive(true);

        ActiveAllShowButton();
    }

    private void ActiveAllShowButton()
    {
        if (!Cube.activeInHierarchy &&
            !Sphere.activeInHierarchy &&
            !Player.activeInHierarchy)
        {
            myUIController.AllShowButtonState(true);
        } else
        {
            myUIController.AllShowButtonState(false);
        }
    }
}
