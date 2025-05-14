using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy m_Prefab;
    [SerializeField] private Player m_Player;
    [SerializeField] private Transform m_Parent;
    [SerializeField] private UnityEvent m_OnEnemyZero;


    [SerializeField] private int m_Count = 2;

    [SerializeField] private float m_Distance = 1;
    [SerializeField] private float m_Delay = 1;
    [SerializeField] private bool m_Respawn = false;

    private List<Enemy> m_Enemys;
    private int m_EnemyDestroy = 0;

    private void Start()
    {
        m_Enemys = new List<Enemy>();
        m_Prefab.gameObject.SetActive(false);
        StartCoroutine(OnGenerator());
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < m_Enemys.Count; i++)
        {
            if (m_Enemys[i] == null)
            {
                if (!m_Respawn)
                    m_EnemyDestroy++;
                m_Enemys.RemoveAt(i--);
                continue;
            }
            m_Enemys[i].OnFixedUpdate();
        }
    }


    private IEnumerator OnGenerator()
    {
        while (true)
        {
            if (m_Enemys.Count + m_EnemyDestroy >= m_Count)
            {
                if (m_Enemys.Count == 0)
                {
                    m_OnEnemyZero?.Invoke();
                    yield return null;
                    break;
                }
                yield return new WaitForSeconds(m_Delay);
                continue;
            }

            Vector2 player = m_Player.position;

            float rand = Random.Range(0, 360f);

            Vector2 position = player + new Vector2(Mathf.Sin(rand), Mathf.Cos(rand)) * m_Distance;

            Enemy enemy = Instantiate(m_Prefab, position, Quaternion.identity, m_Parent);
            enemy.SetComponent(m_Player);
            m_Enemys.Add(enemy);
            enemy.gameObject.SetActive(true);
            yield return new WaitForSeconds(m_Delay);
        }
    }

    public void OnDebug(string text) => Debug.Log(text);
}
