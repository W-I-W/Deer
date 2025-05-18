using UnityEngine;
using UnityEngine.Events;

using static UnityEditor.Searcher.SearcherWindow.Alignment;


public class Player : MonoBehaviour, ICharacterAnimation
{
    [SerializeField] private Rigidbody2D m_Body;

    [SerializeField] private Deer m_Deer;
    [SerializeField] private GameObject m_Wolf;

    [SerializeField] private int m_Weakness = 20;
    [SerializeField] private float _walkSpeed = 2;
    [SerializeField] private float _runSpeed = 4;

    private float m_Horizontal = 0;
    private float m_Vertical = 0;

    private RebuildStates m_State;


    public Vector2Int lastPress { get; set; } = Vector2Int.zero;

    public RebuildStates state => m_State;

    public Vector2 position => transform.position;

    public Deer deer => m_Deer;

    public float horizontal => m_Horizontal;

    public float vertical => m_Vertical;

    public bool isIdle { get; set; }


    public bool isIdleAction = true;

    public UnityAction onIdle { get; set; }

    private void Start()
    {
        RebuildInDeer();
    }

    private void FixedUpdate()
    {
        if (m_Deer.getHealth <= 0) return;

        float time = Time.fixedDeltaTime;

        float acceleration = Input.GetKey(KeyCode.LeftShift) ? 1 : 1.98f;

        var _speed = Input.GetKey(KeyCode.LeftShift) ? _runSpeed : _walkSpeed;

        if (m_Deer.getHealth < m_Weakness)
        {
            _speed = _walkSpeed;
            acceleration = 2;
        }

        m_Horizontal = Input.GetAxisRaw("Horizontal") / acceleration;
        m_Vertical = Input.GetAxisRaw("Vertical") / acceleration;



        m_Body.position += new Vector2(m_Horizontal, m_Vertical).normalized * (_speed * time);
        //Debug.Log("h:" + m_Horizontal + " v:" + m_Vertical + " Idle:" + isIdle);
        //isIdle = Mathf.Approximately(Mathf.RoundToInt( m_Horizontal), 0) && Mathf.Approximately(m_Vertical, 0);
        isIdle = Mathf.RoundToInt(m_Horizontal) == 0 && Mathf.RoundToInt(m_Vertical) == 0;

        if (isIdle)
        {
            if (isIdleAction)
            {
                isIdleAction = false;
                onIdle?.Invoke();
            }
            return;
        }

        isIdleAction = true;
        bool isHorZero = Mathf.RoundToInt(m_Horizontal) == 0;
        int v = isHorZero ? (int)Mathf.Sign(m_Vertical) : 0;
        int h = isHorZero ? 0 : (int)Mathf.Sign(m_Horizontal);
        lastPress = new Vector2Int(h, v);
    }

    public void RebuildInWolf()
    {
        if (m_Deer.getHealth <= 0) return;
        m_State = RebuildStates.Wolf;
        m_Deer.gameObject.SetActive(false);
        m_Wolf.SetActive(true);
    }

    public void RebuildInDeer()
    {
        if (m_Deer.getHealth <= 0) return;
        m_State = RebuildStates.Deer;
        m_Deer.gameObject.SetActive(true);
        m_Wolf.SetActive(false);
    }

    public void TakeDamage(int damage)
    {
        if (m_Deer.getHealth <= 0) return;
        if (m_Deer.gameObject.activeSelf)
            m_Deer.TakeDamage(damage);
    }
}

public enum RebuildStates { Wolf, Deer }
