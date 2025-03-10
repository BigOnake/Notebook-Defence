using UnityEngine;

public class ManagerUI : MonoBehaviour
{
    public static ManagerUI Instance;
    [SerializeField] Canvas UI;
    [SerializeField] GameObject ShopUI;
    [SerializeField] GameObject UpgradeUI;

    private void Start()
    {
        Instance = this;
        DisableShop();
        DisableUpgrades();
    }

    public void DisplayShop()
    {
        EnableShop();
    }

    public void HideShop()
    {
        DisableShop();
    }

    public void DisplayUpgrades()
    {
        EnableUpgrades();
    }

    public void HideUpgrades()
    {
        DisableUpgrades();
    }

    private void DisableShop()
    {
        if (ShopUI.activeSelf)
        {
            ShopUI.SetActive(false);
        }
    }

    private void EnableShop()
    {
        if (!ShopUI.activeSelf)
        {
            UpgradeUI.SetActive(false);
            ShopUI.SetActive(true);
        }
    }

    private void DisableUpgrades()
    {
        if (UpgradeUI.activeSelf)
        {
            UpgradeUI.SetActive(false);
        }
    }

    private void EnableUpgrades()
    {
        if (!UpgradeUI.activeSelf)
        {
            ShopUI.SetActive(false);
            UpgradeUI.SetActive(true);
        }
    }
}
