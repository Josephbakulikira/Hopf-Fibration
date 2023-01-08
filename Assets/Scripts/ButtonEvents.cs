using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonEvents : MonoBehaviour
{
    public GameObject sphere_panel;
    public GameObject on_image;
    public GameObject off_image;

    public void ToggleShowPanel()
    {
        if(sphere_panel.activeSelf != true)
        {
            sphere_panel.SetActive(true);
            on_image.SetActive(false);
            off_image.SetActive(true);

        }
        else
        {
            sphere_panel.SetActive(false);
            on_image.SetActive(true);
            off_image.SetActive(false);
        }
    }
}
