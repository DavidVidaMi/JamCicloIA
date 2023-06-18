using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPTInterface : MonoBehaviour
{
    public GameObject GPTUI;
    public GameObject Crosshair;
    public PlayerInteract playerInteract;
    public PlayerMovementScript playerMovementScript;
    public MouseLook mouseLook;

    private bool active = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T) && !active)
        {
            ChangeToGPTInterface();
        }
    }

    public void ChangeToGPTInterface()
    {
        active = !active;
        if (active)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        GPTUI.SetActive(active);
        Crosshair.SetActive(!active);
        playerInteract.enabled = !active;
        playerMovementScript.enabled = !active;
        mouseLook.enabled = !active;
    }
}
