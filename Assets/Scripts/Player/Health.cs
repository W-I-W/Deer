using System.Collections;
using System.Net.NetworkInformation;

using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private int m_HealthMax = 100;
    [SerializeField] private int m_HealthMin = 0;
    [SerializeField] private int m_HungerDelay = 1;
    [SerializeField] private int m_HungerDamage = 1;
    [SerializeField] private UnityEvent m_OnMin;

    private int m_Health;

    public UnityAction<int> onValue { get; set; }

    public int max => m_HealthMax;

    private void Start()
    {
        m_Health = m_HealthMax;
        onValue?.Invoke(m_Health);
        StartCoroutine(OnHunger());
    }

    private IEnumerator OnHunger()
    {
        while (true)
        {
            TakeDamage(m_HungerDamage);
            yield return new WaitForSeconds(m_HungerDelay);
        }
    }

    public void TakeDamage(int damage)
    {
        m_Health -= damage;
        m_Health = Mathf.Clamp(m_Health, m_HealthMin, m_Health);
        onValue?.Invoke(m_Health);
        if (m_Health <= m_HealthMin)
        {
            m_OnMin?.Invoke();
        }
    }

    public void Add(int value)
    {
        m_Health = Mathf.Clamp(m_Health + value, m_HealthMin, m_HealthMax);
        onValue?.Invoke(m_Health);
    }
}
