using UnityEngine;

public class TileManager : MonoBehaviour
{
    public static TileManager Instance;
    [SerializeField] private bool isSelected;
    [SerializeField] private Tile selectedTile;

    void Start()
    {
        Instance = this;
    }

    public void CreateTower(GameObject towerPrefab)
    {
        if(isSelected && selectedTile)
        {
            selectedTile.AddTower(towerPrefab);
        }
    }

    public void SelectTile(Tile newTile)
    {
        isSelected = true;
        selectedTile = newTile;
    }

    public void UnselectTile()
    {
        isSelected = false;
        selectedTile = null;
    }
}
