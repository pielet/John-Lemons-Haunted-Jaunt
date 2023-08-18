using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchView : MonoBehaviour
{
    public GameObject downCamera, followCamera, eyeCamera;
    public PlayerMovement player;

    void Start()
    {
        SetDefaultView();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();

        if (Input.GetKeyDown(KeyCode.Alpha1))
            SwitchToFollow();

        if (Input.GetKeyDown(KeyCode.Alpha2))
            SwitchToFirstPerson();

        if (Input.GetKeyDown(KeyCode.Alpha3))
            SwitchToDown();
    }

    void SwitchToFollow()
    {
        downCamera.SetActive(false);
        followCamera.SetActive(true);
        eyeCamera.SetActive(false);

        player.ChangeMode("follow", true, followCamera.transform);
    }

    void SwitchToFirstPerson()
    {
        downCamera.SetActive(false);
        followCamera.SetActive(false);
        eyeCamera.SetActive(true);

        player.ChangeMode("follow", false, eyeCamera.transform);
    }

    void SwitchToDown()
    {
        downCamera.SetActive(true);
        followCamera.SetActive(false);
        eyeCamera.SetActive(false);

        player.ChangeMode("down", true);
    }

    public void SetDefaultView()
    {
        SwitchToFollow();
    }
}
