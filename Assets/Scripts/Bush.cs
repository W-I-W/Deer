using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Bush : MonoBehaviour
{
    [SerializeField] private Eat m_EatPrefab;
    [SerializeField] private int m_MaxEats = 2;
    //[SerializeField] private int m_Delay = 1;

    private List<Eat> m_Eats;

    private int m_Chance = 0;

    private void Awake()
    {
        m_Eats = new List<Eat>();

        //StartCoroutine(OnApple());
    }

    //private IEnumerator OnApple()
    //{
    //    while (true)
    //    {
    //        for (int i = 0; i < m_Eats.Count; i++)
    //        {
    //            if (m_Eats[i] == null)
    //            {
    //                m_Eats.RemoveAt(i);
    //                i--;
    //            }
    //        }
    //        if (m_Eats.Count < m_MaxEats)
    //        {
    //            Eat eat = Instantiate(m_EatPrefab, transform.position, Quaternion.identity, transform);
    //            m_Eats.Add(eat);
    //        }
    //        yield return new WaitForSeconds(Random.Range(m_Delay, m_Delay + 2));
    //    }
    //}

    public void OnCreateFood(int chance, int auto)
    {
        for (int i = 0; i < m_Eats.Count; i++)
        {
            if (m_Eats[i] == null)
            {
                m_Eats.RemoveAt(i);
                i--;
            }
        }
        int rand = Random.Range(0, 100);
        if (m_Eats.Count < m_MaxEats && chance + m_Chance >= rand)
        {
            m_Chance = 0;
            Eat eat = Instantiate(m_EatPrefab, transform.position, Quaternion.identity, transform);
            m_Eats.Add(eat);
        }
        m_Chance += auto;
    }
}
