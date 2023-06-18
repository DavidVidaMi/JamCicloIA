using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalBoss : MonoBehaviour
{
    public Transform playerTransform;
    public float speed = 1;

    private AudioSource audioSource;
    public AudioClip audioClip;

    public GameObject youDiedGO;
    public Image youDiedImage;
    public Image youDiedPanel;

    private bool ded = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(audioClip);
    }
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, speed * Time.deltaTime);

        if (ded)
        {
            Color newImageColor = youDiedImage.color;
            newImageColor.a += 0.002f;
            Color newPanelColor = youDiedPanel.color;
            newPanelColor.a += 0.002f;
            youDiedImage.color = newImageColor;
            youDiedPanel.color = newPanelColor;
            if(youDiedPanel.color.a >= 250)
                {
                    Time.timeScale = 0;
                }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if (other.gameObject.CompareTag("Player"))
        {
            ded = true;
            youDiedGO.SetActive(true);
        }
    }

}

