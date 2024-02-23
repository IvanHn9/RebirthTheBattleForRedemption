using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    private Queue<string> sentences;
    private Animator animator;
    private void Start()
    {
        sentences = new Queue<string>();
        animator = GetComponent<Animator>();
    }
    public void StartDialogue(Dialogue dialogue)
    {
        animator.SetBool("Open", true);
        nameText.text = dialogue.name;
        sentences.Clear();
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayeNextSentence();
    }
    public void DisplayeNextSentence()
    {
        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
        if (sentences.Count <= 1) 
        {
            EndDialogue();
            return;
        }
        
        
    }
    public void EndDialogue()
    {
        StartCoroutine(Delay());
    }
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(2);
        animator.SetBool("Open", false);
    }
}
