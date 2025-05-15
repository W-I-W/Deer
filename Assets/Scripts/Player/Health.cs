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

    private int m_Value;

    public UnityAction<int> onValue { get; set; }

    public int max => m_HealthMax;

    public int value => m_Value;

    private void Start()
    {
        m_Value = m_HealthMax;
        onValue?.Invoke(m_Value);
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
        m_Value -= damage;
        m_Value = Mathf.Clamp(m_Value, m_HealthMin, m_Value);
        onValue?.Invoke(m_Value);
        if (m_Value <= m_HealthMin)
        {
            m_OnMin?.Invoke();
        }
    }

    public void Add(int value)
    {
        m_Value = Mathf.Clamp(m_Value + value, m_HealthMin, m_HealthMax);
        onValue?.Invoke(m_Value);
    }
}
