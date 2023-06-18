using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicCube : MonoBehaviour
{
    public string noteValue;
    public AudioClip audioClip;
    private AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public string PlayNote()
    {
        audioSource.PlayOneShot(audioClip);
        return noteValue;
    }
}
