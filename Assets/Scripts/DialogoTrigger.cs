using UnityEngine;

public class DialogoTrigger : MonoBehaviour
{
    public string[] dialogueLines;
    public float detectionRadius = 5f;
    public Transform lookAtTarget; 
    public KeyCode interactKey = KeyCode.E;

    private bool playerInRange = false;
    private bool isInDialogue = false;

    private void Update()
    {
        if (playerInRange && !isInDialogue)
        {
            if (Input.GetKeyDown(interactKey))
            {
                StartDialogue();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    private void StartDialogue()
    {
        isInDialogue = true;
        DialogoManager.Instance.IniciarDialogo(dialogueLines, lookAtTarget);
    }
}