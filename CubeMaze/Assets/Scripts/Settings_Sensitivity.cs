using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings_Sensitivity : MonoBehaviour
{
    public GameObject MenuPaused;
    public GameObject MenuSettings;

    public Slider Slider_sensitivity_x;
    public Slider Slider_sensitivity_y;

    private PlayerMotion horizontal;
    private HeadAndCameraMotion vertical;
    private MenuManager menuManager;

    private void Start()
    {
        menuManager = FindObjectOfType<MenuManager>();
        horizontal = FindObjectOfType<PlayerMotion>();
        vertical = FindObjectOfType<HeadAndCameraMotion>();
    }

    private void Update()
    {
        horizontal.HorizontalRotationSpeed = Slider_sensitivity_x.value * 210f;
        vertical.VerticalRotationSpeed = Slider_sensitivity_y.value * 210f;
    }

    public void Button_Back()
    {
        menuManager.isMenuSettings = false;
        MenuSettings.SetActive(false);
        MenuPaused.SetActive(true);
    }
}
