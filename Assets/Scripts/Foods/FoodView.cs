using UnityEngine;
using UnityEngine.UI;

public class FoodView : MonoBehaviour
{
    [SerializeField] private Image m_Image;
    [SerializeField] private PlayerResource m_Controller;


    private void Start()
    {
        OnView(m_Controller.item);
    }

    private void OnEnable()
    {
        m_Controller.onPoint += OnView;
    }


    private void OnDisable()
    {
        m_Controller.onPoint -= OnView;
    }


    private void OnView(int value)
    {
        m_Image.fillAmount = (float)value / m_Controller.max;
    }
}
