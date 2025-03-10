using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    public UnityEvent onInteraction;

    public virtual void Interact()
    {
        onInteraction.Invoke();
    }
}
