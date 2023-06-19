using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPanel : MonoBehaviour
{
    public Animator ballFloorAnimator;
    public GameObject Ball;
    public AudioClip goodSound;
    public AudioClip badSound;

    public AudioSource playerAudio;

    private AudioSource audioSource;
    private string correctMelody = "MIFASOLDOREMIFA";
    private int noteCounter;
    private string currentNote;
    private string currentMelody = "";

    private float goodSoundDuration = 16f;

    public static MusicPanel instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void CubePlayed(MusicCube currentCube)
    {
        currentNote = currentCube.PlayNote();
        noteCounter++;

        currentMelody += currentNote;

        Debug.Log(currentMelody);

        if(noteCounter >= 7)
        {
            CheckMelody();
        }
    }

    void CheckMelody()
    {
        noteCounter = 0;
        if(currentMelody.Equals(correctMelody))
        {
            playerAudio.Pause();
            StartCoroutine(WaitAndUnpause());
            audioSource.PlayOneShot(goodSound);
            PlayerInteract.instance.melodyPlayedCorrectly = true;
            ballFloorAnimator.SetTrigger("ActivateTrap");
            Ball.GetComponent<Rigidbody>().isKinematic = false;
        }
        else
        {
            audioSource.PlayOneShot(badSound);
        }
        currentMelody = "";
    }

    private IEnumerator WaitAndUnpause()
    {
        yield return new WaitForSeconds(goodSoundDuration);
        playerAudio.UnPause();
    }
}
