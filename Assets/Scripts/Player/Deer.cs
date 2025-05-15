using System.Security.Permissions;

using DamageNumbersPro;

using UnityEngine;

public class Deer : MonoBehaviour
{
    [SerializeField] private Health m_Health;
    [SerializeField] private DamageNumber m_ViewDamage;
    [SerializeField] private DamageNumber m_ViewAdd;

    public int getHealth => m_Health.value;

    public void TakeDamage(int damage)
    {
        m_ViewDamage.Spawn(transform.position, damage);
        m_Health.TakeDamage(damage);
    }

    public void AddHealth(int damage)
    {
        m_ViewAdd.Spawn(transform.position, damage);
        m_Health.Add(damage);
    }
}
