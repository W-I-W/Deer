using UnityEngine;

public class ParticleDestroy : MonoBehaviour
{
    [SerializeField] private ParticleSystem m_Particle;


    private void Update()
    {
        if (m_Particle.isStopped)
            Destroy(gameObject);

    }

}
