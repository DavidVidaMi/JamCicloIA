using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPanel : MonoBehaviour
{
    public Animator ballFloorAnimator;
    public GameObject Ball;

    private string correctMelody = "MIFASOLDOREMIFA";
    private int noteCounter;
    private string currentNote;
    private string currentMelody = "";

    public static MusicPanel instance;

    private void Awake()
    {
        instance = this;
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
            //continua a cansión un cacho
            PlayerInteract.instance.melodyPlayedCorrectly = true;
            ballFloorAnimator.SetTrigger("ActivateTrap");
            Ball.GetComponent<Rigidbody>().isKinematic = false;
        }
        else
        {
            //Sona sonidiño de error
        }
        currentMelody = "";
    }
}
