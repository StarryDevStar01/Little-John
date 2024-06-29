using UnityEngine;
using UnityEngine.Events;

public class Weapon : MonoBehaviour
{
    [Header("Commons:")]
    public WeaponStats statData;

    [SerializeField] private Transform m_shootingPoint;

    [SerializeField] private GameObject m_bulletPrefab;
    [SerializeField] private GameObject m_muzzleFlashPrefab;

    private float m_curFR;
    private int m_curBullets;
    private float m_curReloadTime;

    private bool m_isShooted;
    private bool m_isReloading;

    [Header("Events:")]
    public UnityEvent OnShoot;
    public UnityEvent OnReload;
    public UnityEvent OnReloadDone;

    private void Start()
    {
        LoadStats();
    }

    private void LoadStats()
    {
        if (statData == null) return;

        statData.Load();
        m_curFR = statData.firerate;
        m_curReloadTime = statData.reloadTime;
        m_curBullets = statData.bullets;
    }

    private void Update()
    {
        ReduceFirerate();

        ReduceReloadTime();
    }

    private void ReduceReloadTime()
    {
        if (!m_isReloading) return;

        m_curReloadTime -= Time.deltaTime;

        if (m_curReloadTime > 0) return;

        LoadStats();

        m_isReloading = false;
        m_isShooted = false;
        OnReloadDone?.Invoke();
    }

    private void ReduceFirerate()
    {
        if (!m_isShooted) return;
        m_curFR -= Time.deltaTime;

        if (m_curFR > 0) return;
        m_curFR = statData.firerate;
        m_isShooted = false;
    }

    public void Shoot(Vector3 targetDirection)
    {
        if (m_isShooted || m_shootingPoint == null || m_curBullets <= 0) return;

        if (m_muzzleFlashPrefab)
        {
            var muzzleFlashClone = Instantiate(m_muzzleFlashPrefab, m_shootingPoint.position, transform.rotation);
            muzzleFlashClone.transform.SetParent(m_shootingPoint);
        }

        if (m_bulletPrefab)
        {
            var bulletClone = Instantiate(m_bulletPrefab, m_shootingPoint.position, transform.rotation);
            var projectileComp = bulletClone.GetComponent<Projectile>();

            if(projectileComp != null)
            {
                projectileComp.Damage = statData.damage;
            }
        }

        m_curBullets--;
        m_isShooted = true;
        if(m_curBullets <= 0)
        {
            Reload();
        }

        OnShoot?.Invoke();
    }

    public void Reload()
    {
        m_isReloading = true;

        OnReload?.Invoke();
    }
}
