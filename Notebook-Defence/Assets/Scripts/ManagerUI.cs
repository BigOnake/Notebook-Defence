using UnityEngine;

public class ManagerUI : MonoBehaviour
{
    public static ManagerUI Instance;
    [SerializeField] Canvas shopUI;

    private void Start()
    {
        Instance = this;
        DisableShop();
    }

    public void DisplayTowerShop()
    {
        EnableShop();
    }

    private void DisableShop()
    {
        shopUI.gameObject.SetActive(false);
    }

    private void EnableShop()
    {
        shopUI.gameObject.SetActive(value: true);
    }
}
