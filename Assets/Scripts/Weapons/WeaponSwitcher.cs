using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    [Tooltip("Where to parent the weapon prefab (e.g. hand transform)")]
    public Transform weaponHoldPoint;

    private PlayerInventory inventory;
    private GameObject currentWeaponGO;

    private void Awake()
    {
        inventory = FindObjectOfType<PlayerInventory>();
    }

    private void Start()
    {
        //EquipCurrentWeapon();
    }

    /// <summary>
    /// Call from WeaponSwitchInput, passing its controls instance.
    /// </summary>
    public void EquipCurrentWeapon(PlayerControls controls)
    {
        // Remove old
        if (currentWeaponGO != null)
            Destroy(currentWeaponGO);

        // Spawn new
        var weaponSO = inventory.GetCurrentWeapon();
        if (weaponSO == null) return;

        currentWeaponGO = Instantiate(
            weaponSO.weaponPrefab,
            weaponHoldPoint.position,
            weaponHoldPoint.rotation,
            weaponHoldPoint
        );

        // Bind the correct fire action
        var wc = currentWeaponGO.GetComponent<WeaponController>();
        if (wc != null)
        {
            // If this is slot 0, use FireLeft; if 1, FireRight
            int idx = inventory.currentWeaponIndex;
            /*var action = (idx == 0)            // this is in case i wanted to add a system based on hand
                ? controls.Gameplay.FireLeft
                : controls.Gameplay.FireRight;*/

            var action = controls.Gameplay.FireLeft;
            wc.Initialize(weaponSO, action);
        }
    }
}