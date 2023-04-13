using FastCampus.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueNPC_New : MonoBehaviour, IInteractable
{
    [SerializeField] Dialogue_New _dialogue;

    bool _isStartDialogue = false;

    GameObject _interactGO;

    [SerializeField] float _distance = 2.0f;

    public float Distance => _distance;

    public void Interact(GameObject other)
    {
        float calcDistance = Vector3.Distance(other.transform.position, transform.position);
        if (calcDistance < _distance)
        {
            return;
        }

        if (_isStartDialogue)
        {
            return;
        }

        _interactGO = other;

        DialogueManager_New.Instance.OnEndDialogue += OnEndDialogue;
        _isStartDialogue = true;

        DialogueManager_New.Instance.StartDialogue(_dialogue);
    }

    public void StopInteract(GameObject other)
    {
        _isStartDialogue = false;
    }

    private void OnEndDialogue()
    {
        StopInteract(_interactGO);
    }
}
