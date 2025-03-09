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
        if (shopUI.enabled == true)
        {
            shopUI.enabled = false;
        }
    }

    private void EnableShop()
    {
        if(shopUI.enabled == false)
        {
            shopUI.enabled = true;
        }
    }
}
