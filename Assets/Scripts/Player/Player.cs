using UnityEngine;


public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D m_Body;

    [SerializeField] private Deer m_Deer;
    [SerializeField] private GameObject m_Wolf;

    [SerializeField] private float m_Speed = 1;


    private float m_Horizontal = 0;
    private float m_Vertivcal = 0;

    private RebuildStates m_State;

    public RebuildStates state => m_State;

    public Vector2 position => transform.position;

    private void Start()
    {
        RebuildInDeer();
    }

    private void FixedUpdate()
    {
        float time = Time.fixedDeltaTime;

        m_Horizontal = Input.GetAxis("Horizontal");

        m_Vertivcal = Input.GetAxis("Vertical");

        m_Body.position += new Vector2(m_Horizontal, m_Vertivcal) * time * m_Speed;
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
