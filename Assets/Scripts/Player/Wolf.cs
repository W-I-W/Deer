using System.Collections;
using System.Collections.Generic;


using UnityEngine;


public class Wolf : MonoBehaviour
{
    [SerializeField] private Health m_Health;
    [SerializeField] private int m_AttackDamage = 1;

    [SerializeField] private float AttackDelay = 1;

    private List<Enemy> m_Enemys;

    private void OnEnable()
    {
        m_Enemys = new List<Enemy>();
        StartCoroutine(Attack());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool isEnemy = collision.TryGetComponent(out Enemy enemy);

        if (isEnemy)
        {
            //enemy.TakeDamage(m_AttackDamage);
            m_Enemys.Add(enemy);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        bool isEnemy = collision.TryGetComponent(out Enemy enemy);

        if (isEnemy)
        {
            m_Enemys.Remove(enemy);
        }
    }

    private IEnumerator Attack()
    {
        while (true)
        {
            for (int i = 0; i < m_Enemys.Count; i++)
            {
                if (m_Enemys[i] == null) continue;
                m_Health.Add(m_AttackDamage);
                m_Enemys[i].TakeDamage(m_AttackDamage);
            }
            yield return new WaitForSeconds(AttackDelay);
        }
    }
}
