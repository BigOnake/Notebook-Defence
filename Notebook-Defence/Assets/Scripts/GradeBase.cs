using UnityEngine;

public class GradeBase : MonoBehaviour
{
    [SerializeField] private int maxHealth = 10;
    [SerializeField] private int currHealth = 10;

    private void Start()
    {
        currHealth = maxHealth;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Enemy>(out Enemy enemy))
        {
            currHealth--;
            other.GetComponent<Enemy>()?.ResetEnemy();

            if (currHealth <= 0)
            {
                GameManager.Instance.GameOver();
            }
        }
    }
}
