
using DamageNumbersPro;

using TMPro;

using UnityEngine;
using UnityEngine.Events;


public class Enemy : MonoBehaviour, IComponentHandler<Player>, ICharacterAnimation
{
    [SerializeField] private Rigidbody2D m_Body;
    [SerializeField] private DamageNumber m_ViewDamagePrefab;
    [SerializeField] private TextMeshPro m_TextViewHealth;
    [SerializeField] private Transform m_Spear;
    [SerializeField] private GameObject m_ParticalDie;

    [SerializeField] private int m_Health = 1;
    [SerializeField] private int m_ToTargetDistance = 2;
    [SerializeField] private float m_MoveSpeed = 2;
    private Vector2 m_Move = Vector2.zero;


    private Player m_Player { get; set; }

    public bool isIdle { get; set; } = false;

    public float horizontal => m_Move.x;

    public float vertical => m_Move.y;

    public Vector2Int lastPress { get; set; } = Vector2Int.zero;

    public UnityAction onIdle { get; set; }

    private void Start()
    {
        m_TextViewHealth.text = m_Health.ToString("00");
    }

    private void OnDestroy()
    {
        Instantiate(m_ParticalDie, transform.position, Quaternion.identity);
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
            SpearRotation(m_Player.position - m_Body.position);
            m_Body.position += (m_Player.position - m_Body.position).normalized * m_MoveSpeed * Time.fixedDeltaTime;
            Logic((m_Player.position - m_Body.position).normalized);
            return;
        }
        m_Body.position += (m_Body.position - m_Player.position).normalized * m_MoveSpeed * Time.fixedDeltaTime;
        Logic((m_Body.position - m_Player.position).normalized);
        SpearRotation(m_Body.position - m_Player.position);


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

    private void Logic(Vector2 value)
    {
        //Debug.Log(m_Body.linearVelocity);
        m_Move = value; ;
        isIdle = Mathf.Approximately(m_Move.x, 0) && Mathf.Approximately(m_Move.y, 0);
        if (isIdle) return;
        bool isHorZero = Mathf.Approximately(horizontal, 0);
        int v = isHorZero ? (int)Mathf.Sign(vertical) : 0;
        int h = isHorZero ? 0 : (int)Mathf.Sign(horizontal);
        lastPress = new Vector2Int(h, v);
    }

    public void SpearRotation(Vector2 value)
    {
        Vector2 direciton = value;
        var rotationTarget = Mathf.Atan2(direciton.y, direciton.x) * Mathf.Rad2Deg;
        m_Spear.rotation = Quaternion.Euler(new Vector3(0, 0, rotationTarget));
    }
}

public interface IComponentHandler<T>
{
    public void SetComponent(T player);
    public void OnFixedUpdate();
}
