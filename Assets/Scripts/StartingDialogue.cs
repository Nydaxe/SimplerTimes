using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Collections;

public class StartingDialogue : MonoBehaviour
{
    [SerializeField] GameObject dialoguePanel;
    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] float typeSpeed;
    [SerializeField] AudioSource talkAudio;
    [SerializeField] DialogueLine[] StartingDialogueLines;
    Queue<DialogueLine> lines;
    bool lineFinished = true;

    void Start()
    {
        dialoguePanel.SetActive(false);
    }

    void Update()
    {
        if (dialoguePanel.activeSelf && Input.GetKeyDown(KeyCode.Space))
        {
            DisplayNextMessage();
        }
    }

    public void StartDialogue()
    {
        lines = new Queue<DialogueLine>();
        dialoguePanel.SetActive(true);

        for(int i = 0; i < StartingDialogueLines.Length; i++)
            lines.Enqueue(StartingDialogueLines[i]);

        DisplayNextMessage();
    }

    IEnumerator TypeLine(string line)
    {
        bool audioPlayed = false;

        dialogueText.text = "";

        foreach (char character in line)
        {
            dialogueText.text += character;
            
            if(Random.Range(1,7) == 1 && audioPlayed == false)
            {
                SoundEffectManager.instance.PlaySoundEffect(talkAudio); 

                audioPlayed = true;           
            }
            else
            {
                audioPlayed = false;
            }

            yield return new WaitForSecondsRealtime(typeSpeed);
        }

        lineFinished = true;
    }
    
    void DisplayNextMessage()
    {
        if(lineFinished == false)
        {
            StopAllCoroutines();
            dialogueText.text = "";
            lineFinished = true;
            return;
        }

        dialogueText.text = "";
        if (lines.Count == 0)
        {
            EndDialogue();
            return;
        }
        
        lineFinished = false;
        DialogueLine message = lines.Dequeue();
        StartCoroutine(TypeLine(message.text));
    }

    void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        Time.timeScale = 1;
        SceneFader.instance.EndScene();
    }
}