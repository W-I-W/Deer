using System.Net.NetworkInformation;

using UnityEngine;
using UnityEngine.Events;

public class CharacterAnimation : MonoBehaviour
{
    [SerializeField] private Health m_Health;
    [SerializeField] private Animator m_Animator;

    private ICharacterAnimation m_Character;


    private void Awake()
    {
        m_Character = transform.parent.GetComponent<ICharacterAnimation>() ??
            GetComponent<ICharacterAnimation>();
    }

    private void OnEnable()
    {
        m_Character.onIdle += OnIdle;
    }

    private void OnDisable()
    {
        m_Character.onIdle -= OnIdle;
    }

    private void Update()
    {
        if (m_Health?.value <= 0)
        {
            m_Animator.SetBool("Die", true);
            return;
        }

        m_Animator.SetFloat("XIdle", m_Character.lastPress.x);
        m_Animator.SetFloat("YIdle", m_Character.lastPress.y);
        if (!m_Character.isIdle)
        {
            m_Animator.SetFloat("X", m_Character.horizontal);
            m_Animator.SetFloat("Y", m_Character.vertical);
        }

    }

    private void OnIdle()
    {
        m_Animator.SetFloat("X", m_Character.horizontal);
        m_Animator.SetFloat("Y", m_Character.vertical);
    }
}

public interface ICharacterAnimation
{
    public bool isIdle { get; set; }
    public float vertical { get; }
    public float horizontal { get; }
    public Vector2Int lastPress { get; set; }

    public UnityAction onIdle { get; set; }
}

