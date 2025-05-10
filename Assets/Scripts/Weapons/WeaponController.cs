using UnityEngine;
using UnityEngine.InputSystem;
using Lean.Pool;

[RequireComponent(typeof(WeaponStatsHolder))]
public class WeaponController : MonoBehaviour
{
    [Header("Fire Point")]
    [SerializeField] private Transform firePoint;

    private WeaponSO data;
    private InputAction fireAction;

    private float fireTimer = 0f;
    private float fireInterval = 0.1f;
    private bool isFiring = false;

    /// <summary>
    /// Initialize this weapon with its data and bind the fire input.
    /// </summary>
    public void Initialize(WeaponSO weaponData, InputAction fireInput)
    {
        data       = weaponData;
        fireAction = fireInput;

        // Precompute interval
        fireInterval = data.weaponStats.fireRate;
        fireTimer    = 0; // fire immediately on first press

        // Apply stats
        GetComponent<WeaponStatsHolder>().ApplyStats(data.weaponStats);
    }

    /*private void OnEnable()
    {
        if (fireAction != null)
            fireAction.Enable();
    }

    private void OnDisable()
    {
        if (fireAction != null)
            fireAction.Disable();
    }*/

    private void Update()
    {
        if (fireAction == null) return;

        if(fireInterval != data.weaponStats.fireRate)
            fireInterval = data.weaponStats.fireRate;
        
        // Check if button is held
        isFiring = fireAction.IsPressed();

        fireTimer += Time.deltaTime;
        
        if (isFiring)
        {
            // Fire as many times as needed if frame rate is low
            if (fireTimer >= fireInterval)
            {
                FireOnce();
                fireTimer = 0;
            }
        }
    }

    private void FireOnce()
    {
        // Spawn bullet via LeanPool
        var projectile = LeanPool.Spawn(
            data.bulletPrefab,
            firePoint.position,
            firePoint.rotation
        ).GetComponent<Projectile>();

        projectile.SetDamage(data.weaponStats.damage);
        projectile.ScheduleDespawn();

        // Optional VFX/SFX here
        Debug.Log($"[{data.itemName}] Fired bullet (dmg={data.weaponStats.damage})");
    }
}
