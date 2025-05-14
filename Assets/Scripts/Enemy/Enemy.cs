
using DamageNumbersPro;

using TMPro;

using UnityEngine;

public class Enemy : MonoBehaviour, IComponentHandler<Player>
{
    [SerializeField] private Rigidbody2D m_Body;
    [SerializeField] private DamageNumber m_ViewDamagePrefab;
    [SerializeField] private TextMeshPro m_TextViewHealth;

    [SerializeField] private int m_Health = 1;
    [SerializeField] private int m_ToTargetDistance = 2;
    [SerializeField] private float m_MoveSpeed = 2;


    private Player m_Player { get; set; }


    private void Start()
    {
        m_TextViewHealth.text = m_Health.ToString("00");
    }

    public void SetComponent(Player player)
    {
        m_Player = player;
    }

    public void OnFixedUpdate()
    {
        if (Vector2.Distance(m_Body.position, m_Player.position) > m_ToTargetDistance) return;
        if (m_Player.state == RebuildStates.Deer)
        {
            m_Body.position += (m_Player.position - m_Body.position).normalized * m_MoveSpeed * Time.fixedDeltaTime;
            return;
        }
        m_Body.position += (m_Body.position - m_Player.position).normalized * m_MoveSpeed * Time.fixedDeltaTime;
    }

    public void TakeDamage(int damage)
    {
        m_Health -= damage;
        m_TextViewHealth.text = m_Health.ToString("00");
        m_ViewDamagePrefab.Spawn(transform.position, damage);
        if (m_Health <= 0)
            Destroy(gameObject);
    }

    public void AddHealth(int health)
    {
        m_Health += health;
        m_TextViewHealth.text = m_Health.ToString("00");
    }
}

public interface IComponentHandler<T>
{
    public void SetComponent(T player);
    public void OnFixedUpdate();
}
