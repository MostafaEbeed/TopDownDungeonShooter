using UnityEngine;

// Optional: cleanly cache stats for use in other parts of the prefab (e.g. recoil, VFX)
public class WeaponStatsHolder : MonoBehaviour
{
    [HideInInspector] public float fireRate;
    [HideInInspector] public int damage;
    [HideInInspector] public int ammoCapacity;

    public void ApplyStats(WeaponStats stats)
    {
        fireRate      = stats.fireRate;
        damage        = stats.damage;
        ammoCapacity  = stats.ammoCapacity;
    }
}