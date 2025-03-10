using UnityEngine;

public class WaveController : MonoBehaviour
{
    [SerializeField] private Spawner enemySpawner;

    void Start()
    {
        enemySpawner.GetComponent<Spawner>();
    }

    void Update()
    {
        
    }
}
