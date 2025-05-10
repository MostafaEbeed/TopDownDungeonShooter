using UnityEngine;
using Lean.Pool;

[RequireComponent(typeof(Collider))]
public class Projectile : MonoBehaviour
{
    [Header("Movement")]
    [Tooltip("Speed of the projectile in units/second")]
    [SerializeField] private float speed = 20f;

    [Tooltip("Time in seconds before the projectile auto-destroys")]
    [SerializeField] private float lifetime = 5f;

    private int damage;

    /// <summary>
    /// Called by WeaponController to set the damage this projectile will deal.
    /// </summary>
    public void SetDamage(int dmg)
    {
        damage = dmg;
    }

    private void Start()
    {
        // Ensure collider is a trigger so it doesn't block movement
        var col = GetComponent<Collider>();
        col.isTrigger = true;

        // Destroy after lifetime expires
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        // Move forward each frame
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Attempt to deal damage to a Health component
        /*var health = other.GetComponent<Health>();
        if (health != null)
        {
            health.TakeDamage(damage);
        }*/

        // Optionally: play impact VFX/SFX here

        // Despawn via Lean Pool
        LeanPool.Despawn(gameObject);
    }
    
    /// <summary>
    /// Call this to automatically despawn after `lifetime` seconds.
    /// </summary>
    public void ScheduleDespawn()
    {
        LeanPool.Despawn(gameObject, lifetime);
    }
}