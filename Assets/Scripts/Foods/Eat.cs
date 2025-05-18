using UnityEngine;
using UnityEngine.Rendering;

public class Eat : MonoBehaviour
{

    [SerializeField] private Rigidbody2D m_Body;
    [SerializeField] private int m_IsAnimationImpulse = 10;
    [SerializeField] protected int m_Eat = 1;

    protected virtual void OnEnable()
    {
        m_Body.AddForce(Random.insideUnitCircle * m_IsAnimationImpulse);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Ontrigger(collision);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Ontrigger(collision);
    }

    protected virtual void Ontrigger(Collider2D collision)
    {
        bool isPlayer = collision.TryGetComponent(out Player player);

        if (isPlayer)
        {
            if (player.state == RebuildStates.Wolf) return;

            player.deer.AddHealth(m_Eat);
            Destroy(gameObject);
        }
    }
}


