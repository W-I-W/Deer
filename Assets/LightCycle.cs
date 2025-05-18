using System.Collections;

using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightCycle : MonoBehaviour
{
    [SerializeField] private Light2D _light;

    [SerializeField] private float _step = .00093f;
    [SerializeField] private float _delayForHalf = 3f;
    [SerializeField] private float _intensityForDay = 0.8f;
    [SerializeField] private float _intensityForNight = 0.2f;


    private void OnDisable()
    {
        StopAllCoroutines();
    }

    public IEnumerator OnDay()
    {
        yield return new WaitForSeconds(_delayForHalf);
        while (_light.intensity < _intensityForDay)
        {
            _light.intensity += _step * Time.deltaTime;
            yield return null;
        }
    }

    public IEnumerator OnNight()
    {
        yield return new WaitForSeconds(_delayForHalf);
        while (_light.intensity > _intensityForNight)
        {
            _light.intensity -= _step * Time.deltaTime;
            yield return null;
        }
    }
    public void StartDay()
    {
        StopAllCoroutines();
        if (transform.parent.gameObject.activeSelf)
            StartCoroutine(OnNight());

    }
    public void StartNight()
    {
        StopAllCoroutines();
        if (transform.parent.gameObject.activeSelf)
            StartCoroutine(OnDay());
    }
}
