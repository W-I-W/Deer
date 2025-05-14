using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class HealthView : MonoBehaviour
{
    [SerializeField] private Health m_Health;
    [SerializeField] private TextMeshProUGUI m_Text;
    [SerializeField] private Image m_Image;

    private void OnEnable()
    {
        m_Health.onValue += OnViewValue;
    }

    private void OnDisable()
    {
        m_Health.onValue -= OnViewValue;
    }


    private void OnViewValue(int value)
    {
        m_Text.text = value.ToString("00");
        m_Image.fillAmount = (float)value / m_Health.max;
    }
}
