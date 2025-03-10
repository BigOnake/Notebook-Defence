using UnityEngine;
using System;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance;
    [SerializeField] private int money = 50;
    [SerializeField] private int maxMoney = 50;
    [SerializeField] private int minMoney = 50;

    public Action<GameObject> onTurretPurchase;

    void Start()
    {
        Instance = this;
    }

    public void BuyTurret(GameObject turretPrefab)
    {
        int price = -1;

        if (turretPrefab.GetComponent<Turret>())
        {
            price = turretPrefab.GetComponent<Turret>().GetPrice();

            Debug.Log("price is " + price);
        }
        else
        {
            Debug.Log("Turret is " + turretPrefab.GetComponent<Turret>());
        }

        if (price != -1 && price <= money)
        {
            money -= price;
            onTurretPurchase.Invoke(turretPrefab);
        }
    }
}
