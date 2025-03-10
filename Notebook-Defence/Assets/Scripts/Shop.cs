using UnityEngine;

public class Shop : MonoBehaviour
{
    public static Shop Instance;
    [SerializeField] private int money = 50;
    [SerializeField] private int maxMoney = 50;
    [SerializeField] private int minMoney = 50;

    private void Start()
    {
        Instance = this;
    }

}
