using UnityEngine;


public class Player : MonoBehaviour, ICharacterAnimation
{
    [SerializeField] private Rigidbody2D m_Body;

    [SerializeField] private Deer m_Deer;
    [SerializeField] private GameObject m_Wolf;

    [SerializeField] private float m_Speed = 1;
    [SerializeField] private int m_Weakness = 20;


    private float m_Horizontal = 0;
    private float m_Vertical = 0;
    private float m_Acceleration = 2;

    private RebuildStates m_State;


    public Vector2Int lastPress { get; set; } = Vector2Int.zero;

    public RebuildStates state => m_State;

    public Vector2 position => transform.position;

    public Deer deer => m_Deer;

    public float horizontal => m_Horizontal;

    public float vertical => m_Vertical;

    public bool isIdle { get; set; }

    private void Start()
    {
        RebuildInDeer();
    }

    private void FixedUpdate()
    {
        if (m_Deer.getHealth <= 0) return;
        float time = Time.fixedDeltaTime;

        m_Acceleration = Input.GetKey(KeyCode.LeftShift) ? 1 : 2;
        m_Acceleration = m_Deer.getHealth > m_Weakness ? m_Acceleration : 2;
        m_Horizontal = Input.GetAxis("Horizontal") / m_Acceleration;

        m_Vertical = Input.GetAxis("Vertical") / m_Acceleration;

        m_Body.position += new Vector2(m_Horizontal, m_Vertical) * time * m_Speed;
        //Debug.Log("h:" + m_Horizontal + " v:" + m_Vertical + " Idle:" + isIdle);
        isIdle = Mathf.Approximately(m_Horizontal, 0) && Mathf.Approximately(m_Vertical, 0);
        if (isIdle) return;

        bool isHorZero = Mathf.Approximately(m_Horizontal, 0);
        int v = isHorZero ? (int)Mathf.Sign(m_Vertical) : 0;
        int h = isHorZero ? 0 : (int)Mathf.Sign(m_Horizontal);
        lastPress = new Vector2Int(h, v);
    }

    public void RebuildInWolf()
    {
        m_State = RebuildStates.Wolf;
        m_Deer.gameObject.SetActive(false);
        m_Wolf.SetActive(true);
    }

    public void RebuildInDeer()
    {
        m_State = RebuildStates.Deer;
        m_Deer.gameObject.SetActive(true);
        m_Wolf.SetActive(false);
    }

    public void TakeDamage(int damage)
    {
        if (m_Deer.gameObject.activeSelf)
            m_Deer.TakeDamage(damage);
    }
}

public enum RebuildStates { Wolf, Deer }
