using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    
public GameObject mrLandlord;
public GameObject textBox;
public ParticleSystem moneyExplosion;

[System.Serializable]
public class DialogueSegment
{
    public string SubjectText;

    [TextArea]
    public string DialogueToPrint;
    public bool Skippable;

    //sets a range for how fast text speed can come out
    [Range(1f,25f)]
    public float LettersPerSecond;
}

[SerializeField] private DialogueSegment[] DialogueSegments;
[Space]
[SerializeField] private TMP_Text SubjectText;
[SerializeField] private TMP_Text BodyText;

private int DialogueIndex;
private bool PlayingDialogue;
private bool Skip;

    void Start()
    {
        //finds the necessary game objects within the scene
    mrLandlord = GameObject.Find("Mr.Landlord");
    textBox = GameObject.Find("DialogueCanvas");
    moneyExplosion = GameObject.Find("MoneyExplosion").GetComponent<ParticleSystem>();


    //initiates the dialogue when the game starts
        StartCoroutine(PlayDialogue(DialogueSegments[DialogueIndex]));
    }

    void Update()
    {
        //when the E key is pressed, it begins to print the dialogue, then checks if there is more dialogue to print and if so, prints that
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(DialogueIndex == DialogueSegments.Length)
            {
                enabled = false;
                return;
            }

            if(!PlayingDialogue)
            {
                StartCoroutine(PlayDialogue(DialogueSegments[DialogueIndex]));
            }
            else
            {
                //checks if dialogue can be skipped and if it can, skips it
                if(DialogueSegments[DialogueIndex].Skippable)
                {
                    Skip = true;
                }
            }
        }
        //if checks if there is any more dialogue and if there isnt, makes the speaker and textbox inactive
        if(DialogueIndex >= DialogueSegments.Length && Input.GetKeyDown(KeyCode.E))
        {
            textBox.SetActive(false);
            mrLandlord.SetActive(false);
        }
    }

    //this function prints the text at the set speed and, if the text is skipped, measures how much to display
    private IEnumerator PlayDialogue(DialogueSegment segment)
    {
        PlayingDialogue = true;
        
        BodyText.SetText(string.Empty);
        SubjectText.SetText(segment.SubjectText);

        float delay = 1f / segment.LettersPerSecond;
        for(int i = 0; i < segment.DialogueToPrint.Length; i++)
        {
            if(Skip)
            {
                BodyText.SetText(segment.DialogueToPrint);
                Skip = false;
                break;
            }

            string chunkToAdd = string.Empty;
            chunkToAdd += segment.DialogueToPrint[i];
            if(segment.DialogueToPrint[i] == ' ' && i < segment.DialogueToPrint.Length - 1)
            {
                chunkToAdd = segment.DialogueToPrint.Substring(i, 2);
                i++;
            }

            BodyText.text += chunkToAdd;
            yield return new WaitForSeconds(delay);
        }

        PlayingDialogue = false;
        DialogueIndex++;

        //if checks if there is any more dialogue and if there isnt, makes the speaker and textbox inactive
     if (DialogueIndex >= DialogueSegments.Length)
     {
     yield return new WaitForSeconds(2f);

    textBox.SetActive(false);
    mrLandlord.SetActive(false);
    moneyExplosion.Play();


     }
    }
}
