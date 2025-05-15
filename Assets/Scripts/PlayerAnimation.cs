using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator m_Animator;
    [SerializeField] private Player m_Player;

    private void Update()
    {
        m_Animator.SetFloat("x", m_Player.horizontal);
        m_Animator.SetFloat("y", m_Player.vertical);

    }
}
