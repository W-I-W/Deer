using UnityEngine;


public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D m_Body;

    [SerializeField] private Deer m_Deer;
    [SerializeField] private GameObject m_Wolf;

    [SerializeField] private float m_Speed = 1;


    private float m_Horizontal = 0;
    private float m_Vertical = 0;
    private float m_Acceleration = 2;

    private RebuildStates m_State;

    public float horizontal => m_Horizontal;

    public float vertical => m_Vertical;

    public Vector2Int lastPress { private set; get; } = Vector2Int.zero;

    public RebuildStates state => m_State;

    public Vector2 position => transform.position;

    public Deer deer => m_Deer;

    private void Start()
    {
        RebuildInDeer();
    }

    private void FixedUpdate()
    {
        float time = Time.fixedDeltaTime;

        m_Acceleration = Input.GetKeyUp(KeyCode.Space) ? 2 : 1;

        m_Horizontal = Input.GetAxis("Horizontal") / m_Acceleration;

        m_Vertical = Input.GetAxis("Vertical") / m_Acceleration;

        m_Body.position += new Vector2(m_Horizontal, m_Vertical) * time * m_Speed;

        if (m_Horizontal == 0 && m_Vertical == 0) return;

        bool isHorZero = Mathf.Approximately(m_Horizontal, 0);
        bool isVerZero = Mathf.Approximately(m_Horizontal, 0);
        Debug.Log(isHorZero);
        lastPress = new Vector2Int();
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
