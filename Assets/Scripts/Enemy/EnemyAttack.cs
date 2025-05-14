using System.Collections;
using System.Drawing.Imaging;

using TMPro;

using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private Enemy m_Enemy;
    [SerializeField] private float m_Delay = 0.1f;
    [SerializeField] private int m_AttackDamag = 1;


    private Player m_Player = null;

    private void Start()
    {
        StartCoroutine(OnAttack());
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool isPlayer = collision.TryGetComponent(out Player player);

        if (isPlayer)
        {
            m_Player = player;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        bool isPlayer = collision.TryGetComponent(out Player player);

        if (isPlayer)
        {
            m_Player = null;
        }
    }

    private IEnumerator OnAttack()
    {
        while (true)
        {
            if (m_Player == null || m_Player.state == RebuildStates.Wolf)
            {
                yield return new WaitForSeconds(m_Delay);
                continue;
            }

            m_Player.TakeDamage(m_AttackDamag);
            m_Enemy.AddHealth(m_AttackDamag);
            yield return new WaitForSeconds(m_Delay);

        }
    }
}
