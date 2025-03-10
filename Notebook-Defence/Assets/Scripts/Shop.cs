using NUnit.Framework;
using System;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameObject turretPrefab;

    public void BuyTurret()
    {
        if (turretPrefab)
            MoneyManager.Instance.BuyTurret(turretPrefab);
        else
            Debug.Log(this.name + " doesn't have a turret prefub");
    }
}
