using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.WSA;

public class Interaction : MonoBehaviour
{
    private Vector2 origin, direction;
    private RaycastHit2D ray;
    public LayerMask layerToHit;
    public Tile selectedTile;
    [SerializeField] Interactable currentInteractable;

    void Update()
    {
        if(!EventSystem.current.IsPointerOverGameObject())
        {
            CastRay();

            if (Input.GetMouseButtonDown(0))
            {
                CheckInteraction();

                if (currentInteractable)
                {
                    currentInteractable.Interact();
                }
                else
                {
                    selectedTile = null;
                    TileManager.Instance.UnselectTile();
                }
            }
        }
        else 
        {
            Debug.Log("Mouse is over UI");

            if(Input.GetMouseButtonDown(0) && selectedTile)
            {
                TileManager.Instance.SelectTile(selectedTile);
            }
        }
    }

    private void CheckInteraction()
    {
        if (ray)
        {
            Interactable newInteractable = ray.collider.GetComponent<Interactable>();

            if (newInteractable.enabled)
            {
                SetNewCurrentInteractable(newInteractable);
            }
        }
        else
        {
            DisableCurrentInteractable();
        }
    }

    private void CastRay()
    {
        origin = Camera.main.ScreenPointToRay(Input.mousePosition).origin;
        direction = Camera.main.ScreenPointToRay(Input.mousePosition).direction;
        ray = Physics2D.Raycast(origin, direction, Mathf.Infinity, layerToHit); //Casts a ray from camera to the mouse position.
    }

    private void SetNewCurrentInteractable(Interactable newInteractable)
    {
        currentInteractable = newInteractable;
        selectedTile = currentInteractable.GetComponent<Tile>();
        Debug.Log(currentInteractable.gameObject.name + " is current interactable");
    }

    private void DisableCurrentInteractable()
    {
        if(currentInteractable)
        {
            currentInteractable = null; 
        }
    }
}
