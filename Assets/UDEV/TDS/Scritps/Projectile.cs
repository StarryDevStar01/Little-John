using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Base Settings:")]
    [SerializeField] private float m_speed;
    private float m_damage;

    private float m_curSpeed;

    [SerializeField] private GameObject m_bodyHitPrefab;

    private Vector2 m_lastPosition;
    private RaycastHit2D m_raycastHit;

    public float Damage { get => m_damage; set => m_damage = value; }

    private void Start()
    {
        m_curSpeed = m_speed;
        RefreshLastPos();
    }

    private void Update()
    {
        transform.Translate(transform.right * m_curSpeed * Time.deltaTime, Space.World);

        DealDamage();
        RefreshLastPos();
    }

    private void DealDamage()
    {
        Vector2 rayDirection = (Vector2)transform.position - m_lastPosition;
        m_raycastHit = Physics2D.Raycast(m_lastPosition, rayDirection, rayDirection.magnitude);
        var collider = m_raycastHit.collider;

        if (!m_raycastHit || collider == null) return;

        if (collider.CompareTag(TagConsts.ENEMY_TAG))
        {
            DealDamageToEnemy(collider);
        }
    }

    private void DealDamageToEnemy(Collider2D collider)
    {
        Actor actorComp = collider.GetComponent<Actor>();

        actorComp?.TakeDamage(m_damage);

        if (m_bodyHitPrefab)
        {
            Instantiate(m_bodyHitPrefab, (Vector3)m_raycastHit.point, Quaternion.identity);
        }

        Destroy(gameObject);
    }

    private void RefreshLastPos()
    {
        m_lastPosition = (Vector2) transform.position;
    }

    private void OnDisable()
    {
        m_raycastHit = new RaycastHit2D();
    }
}
