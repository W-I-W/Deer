using UnityEngine;
using UnityEngine.Events;

public class TriggerWin : MonoBehaviour
{
    [SerializeField] private UnityEvent m_OnEvent;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool isWin = collision.TryGetComponent(out Player player);
        if(isWin)
        {
            m_OnEvent?.Invoke();
        }
    }



}
