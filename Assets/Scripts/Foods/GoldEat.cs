using UnityEngine;

public class GoldEat : Eat
{
    [SerializeField] private int m_Point = 1;

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void Ontrigger(Collider2D collision)
    {
        bool isPlayer = collision.TryGetComponent(out Player player);

        if (isPlayer)
        {
            if (player.state == RebuildStates.Wolf) return;

            player.deer.AddHealth(m_Eat);
            player.deer.inventory.item += m_Point;
            Destroy(gameObject);
        }
    }
}
