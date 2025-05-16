using UnityEngine;
using UnityEngine.Events;

public class PlayerResource : MonoBehaviour
{
    [SerializeField] private int m_Items = 0;
    [SerializeField] private int m_MaxItems = 1;

    public UnityAction<int> onPoint { get; set; }

    public int max => m_MaxItems;

    [SerializeField] private UnityEvent m_OnWin;


    public int item
    {
        get => m_Items;
        set
        {
            m_Items = value > m_MaxItems ? m_MaxItems : value;
            if (m_Items >= m_MaxItems)
                m_OnWin?.Invoke();
            onPoint?.Invoke(m_Items);
        }
    }
}
