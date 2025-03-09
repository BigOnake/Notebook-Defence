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
            Debug.Log("Disaply Mods");
            onEmpty.Invoke();
        }
        else
        {
            Debug.Log("Display Shop");
            onOccupied.Invoke();
        }
    }

    public void AddTower(GameObject prefab)
    {
        isOccupied = !isOccupied;
        tower = Instantiate(prefab, transform.position, transform.rotation);
    }
}
