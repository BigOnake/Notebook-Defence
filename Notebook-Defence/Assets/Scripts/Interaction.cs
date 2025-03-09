using System;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    private Vector2 origin, direction;
    public LayerMask layerToHit;
    [SerializeField] Interactable currentInteractable;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            checkInteraction();
        }
    }

    private void checkInteraction()
    {
        origin = Camera.main.ScreenPointToRay(Input.mousePosition).origin;
        direction = Camera.main.ScreenPointToRay(Input.mousePosition).direction;
        RaycastHit2D ray = Physics2D.Raycast(origin, direction, 1000f, layerToHit); //Casts a ray from camera to the mouse position.

        if (ray)
        {
            Interactable newInteractable = ray.collider.GetComponent<Interactable>();

            if(newInteractable.enabled)
            {
                SetNewCurrentInteractable(newInteractable);
            }
        }
        else
        {
            DisableCurrentInteractable();
        }
    }

    private void SetNewCurrentInteractable(Interactable newInteractable)
    {
        currentInteractable = newInteractable;
        currentInteractable.EnableOutline();
    }

    private void DisableCurrentInteractable()
    {
        if(currentInteractable)
        {
            currentInteractable.DisableOutline();
            currentInteractable = null; 
        }
    }
}
