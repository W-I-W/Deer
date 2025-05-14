using DamageNumbersPro;

using UnityEngine;

public class Deer : MonoBehaviour
{
    [SerializeField] private Health m_Health;
    [SerializeField] private DamageNumber m_ViewDamage;


    public void TakeDamage(int damage)
    {
        m_ViewDamage.Spawn(transform.position, damage);
        m_Health.TakeDamage(damage);
    }
}
