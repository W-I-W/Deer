using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator m_Animator;

    private ICharacterAnimation m_Character;


    private void Start()
    {
        m_Character = transform.parent.GetComponent<ICharacterAnimation>() ??
            GetComponent<ICharacterAnimation>();
    }

    private void Update()
    {
        m_Animator.SetFloat("XIdle", m_Character.lastPress.x);
        m_Animator.SetFloat("YIdle", m_Character.lastPress.y);
        if (!m_Character.isIdle)
        {
            m_Animator.SetFloat("X", m_Character.horizontal);
            m_Animator.SetFloat("Y", m_Character.vertical);
        }
    }
}

public interface ICharacterAnimation
{
    public bool isIdle { get; set; }
    public float vertical { get; }
    public float horizontal { get; }
    public Vector2Int lastPress { get; set; }


}

