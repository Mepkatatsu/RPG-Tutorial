using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager_New : MonoBehaviour
{
    private static DialogueManager_New _instance;

    public TMP_Text _nameText;
    public TMP_Text _dialogueText;

    public Animator _animator = null;

    private Queue<string> _sentences;

    public event Action OnStartDialogue;
    public event Action OnEndDialogue;

    public static DialogueManager_New Instance => _instance;

    private void Awake()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        _sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue_New dialogue)
    {
        OnStartDialogue?.Invoke();

        _animator?.SetBool("IsOpen", true);

        _nameText.text = dialogue.name;

        _sentences.Clear();

        foreach (string sentence in dialogue._sentences)
        {
            _sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (_sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = _sentences.Dequeue();
        StopAllCoroutines();

        StartCoroutine(TypeSentence(sentence));
    }

    private IEnumerator TypeSentence(string sentence)
    {
        _dialogueText.text = string.Empty;

        yield return new WaitForSeconds(0.25f);

        foreach (char letter in sentence.ToCharArray())
        {
            _dialogueText.text += letter;
            yield return null;
        }
    }

    private void EndDialogue()
    {
        _animator?.SetBool("IsOpen", false);
        OnEndDialogue?.Invoke();
    }
}
