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

    private void Update()
    {
        MoneyManager.Instance.onTurretPurchase += CreateTower;
    }

    public void CreateTower(GameObject towerPrefab)
    {
        if(isSelected && selectedTile)
        {
            selectedTile.AddTower(towerPrefab);
        }
        else
        {
            Debug.Log("Tile is not selected");
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
