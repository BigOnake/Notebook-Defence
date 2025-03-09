using UnityEngine;

public class TileInteraction : MonoBehaviour
{
    private Vector2 origin, direction;
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CheckForCollision();
        }
    }

    private void CheckForCollision()
    {
        origin = Camera.main.ScreenPointToRay(Input.mousePosition).origin;
        direction = Camera.main.ScreenPointToRay(Input.mousePosition).direction;
        RaycastHit2D ray = Physics2D.Raycast(origin,direction); //Casts a ray from camera to the mouse position.

        if (ray)
        {
            //TODO: Implemet opening of tower selection menu
            Debug.Log(ray.collider.gameObject.name + " Is Hit");
        }
    }
}
