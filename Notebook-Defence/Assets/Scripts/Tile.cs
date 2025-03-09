using UnityEngine;
using UnityEngine.Events;

public class Tile : MonoBehaviour
{
    public bool isOccupied;
    public UnityEvent onEmpty, onOccupied;
    
    void Start()
    {
        isOccupied = false;
    }

    public void ClickTile()
    {
        if(!isOccupied)
        {
            Debug.Log("Disaply Mods");
            onEmpty.Invoke();
        }
        else
        {
            Debug.Log("Display Shop");
            onOccupied.Invoke();
        }
    }

    public void changeFlag()
    {
        isOccupied = !isOccupied;
    }
}
