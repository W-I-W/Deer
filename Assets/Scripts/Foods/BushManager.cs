
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class BushManager : MonoBehaviour
{
    [SerializeField] private float m_Delay = 0.1f;
    [Range(0, 100)]
    [SerializeField] private int m_Chance = 60;
    [Range(0, 100)]
    [SerializeField] private int m_ChanceGold = 60;
    [SerializeField] private int m_ChanceAuto = 1;

    private List<Bush> m_Bushs;

    private void Start()
    {
        m_Bushs = new List<Bush>();
        for (int i = 0; i < transform.childCount; i++)
        {
            bool isBush = transform.GetChild(i).TryGetComponent(out Bush bush);
            if (isBush)
            {
                m_Bushs.Add(bush);
            }
        }
        StartCoroutine(OnUpdate());
    }
    private IEnumerator OnUpdate()
    {
        while (true)
        {
            for (int i = 0; i < m_Bushs.Count; i++)
            {
                m_Bushs[i].OnCreateFood(m_Chance, m_ChanceAuto, m_ChanceGold);
                yield return new WaitForSeconds(m_Delay);
            }
        }
    }
}
