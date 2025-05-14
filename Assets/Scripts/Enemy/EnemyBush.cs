using UnityEngine;

public class EnemyBush : MonoBehaviour
{
    [SerializeField] private EnemySpawner m_Spawner;
    [Range(0, 100)]
    [SerializeField] private int m_Change = 100;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        int change = Random.Range(0, 100);
        if (change > m_Change) return;

        bool isPlayer = collision.TryGetComponent(out Player player);

        if (isPlayer)
        {
            m_Spawner.CreateEnemy(transform.position);
        }

    }
}
