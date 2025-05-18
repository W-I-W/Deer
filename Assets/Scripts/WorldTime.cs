using System.Collections;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class WorldTime : MonoBehaviour
{
    [SerializeField] private RectTransform m_Rect;
    [SerializeField] private float m_Step = 1f;
    [SerializeField] private float m_Delay = 0.001f;
    [SerializeField] private UnityEvent m_OnDay;
    [SerializeField] private UnityEvent m_OnNight;
    
    [SerializeField] private UnityEvent m_OnMidDay;
    [SerializeField] private UnityEvent m_OnMidNight;
    private bool m_IsDay = true;

    private void Start()
    {
        StartCoroutine(OnNext());
        m_OnDay?.Invoke();
    }

    private IEnumerator OnNext()
    {
        while (true)
        {
            if (m_Rect.anchoredPosition.x <= -m_Rect.sizeDelta.x)
            {
                Transform next = transform.GetChild(0);
                next.SetAsLastSibling();
                m_Rect.anchoredPosition = Vector2.zero;
                
                if (m_IsDay)
                {
                    m_OnNight?.Invoke();
                    m_IsDay = false;
                }
                
                else
                {
                    m_OnDay?.Invoke();
                    m_IsDay = true;
                }
            }
            m_Rect.anchoredPosition -= new Vector2(m_Step*Time.deltaTime, 0);
            //yield return new WaitForSeconds(m_Delay);
            yield return null;
        }
    }

    public void DebugLog(string text)
    {
        Debug.Log(text);
    }
}
