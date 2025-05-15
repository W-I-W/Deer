using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator m_Animator;
    [SerializeField] private Player m_Player;

    private void Update()
    {
        if (m_Player.isIdle)
        {
            m_Animator.SetFloat("XIdle", m_Player.lastPress.x);
            m_Animator.SetFloat("YIdle", m_Player.lastPress.y);
            return;
        }
        m_Animator.SetFloat("X", m_Player.horizontal);
        m_Animator.SetFloat("Y", m_Player.vertical);
    }
}
