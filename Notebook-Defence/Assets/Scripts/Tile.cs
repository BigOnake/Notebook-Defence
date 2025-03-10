using UnityEngine;
using UnityEngine.Events;

public class Tile : MonoBehaviour
{
    public bool isOccupied;
    public UnityEvent onEmpty, onOccupied;
    private GameObject tower;
    
    void Start()
    {
        isOccupied = false;
    }

    public void ClickTile()
    {
        if(!isOccupied)
        {
            Debug.Log("Clicked on available");
            onEmpty.Invoke();
        }
        else
        {
            Debug.Log("Clicked on occupied");
            onOccupied.Invoke();
        }
    }

    public void AddTower(GameObject prefab)
    {
        if(!isOccupied)
        { 
            tower = Instantiate(prefab, transform.position, transform.rotation); 
        }

        isOccupied = true;
    }
}
