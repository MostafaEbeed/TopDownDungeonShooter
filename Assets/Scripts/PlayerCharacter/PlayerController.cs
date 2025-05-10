using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerMovement))]
public class PlayerController : MonoBehaviour
{
    private PlayerControls controls;
    private PlayerMovement movement;
    private TimeSlowManager timeSlow;
    private PlayerInventory inventory;
    private WeaponSwitcher switcher;

    private bool isHoldingAbility = false;
    private bool isHoldingGadget = false;

    private void Awake()
    {
        controls = new PlayerControls();
        movement = GetComponent<PlayerMovement>();
        timeSlow = GetComponent<TimeSlowManager>();
        switcher    = GetComponent<WeaponSwitcher>();
        inventory   = FindObjectOfType<PlayerInventory>();
        
        // Movement
        controls.Gameplay.Move.performed += ctx => movement.SetMoveInput(ctx.ReadValue<Vector2>());
        controls.Gameplay.Move.canceled  += ctx => movement.SetMoveInput(Vector2.zero);

        // Firing
        controls.Gameplay.FireLeft.performed += _ => Fire(Hand.Left);
        controls.Gameplay.FireRight.performed += _ => Fire(Hand.Right);

        // Inventory toggle (make sure you have this action in your InputActions)
        controls.Gameplay.ToggleInventory.performed += _ => inventory.ToggleInventoryUI();
        
        //Weapon Switching
        controls.Gameplay.SwitchWeaponNext.performed += _ => OnNext();
        controls.Gameplay.SwitchWeaponPrevious.performed += _ => OnPrev();
        
        // Abilities
        controls.Gameplay.HoldAbility.performed += _ =>
        {
            isHoldingAbility = true;
            timeSlow.EnableSlowTime();
            ShowAbilityWheel(true);
        };
        controls.Gameplay.HoldAbility.canceled += _ =>
        {
            isHoldingAbility = false;
            timeSlow.DisableSlowTime();
            ShowAbilityWheel(false);
        };
        controls.Gameplay.Ability1.performed += _ => { if (isHoldingAbility) ActivateAbility(0); };
        controls.Gameplay.Ability2.performed += _ => { if (isHoldingAbility) ActivateAbility(1); };

        // Gadgets
        controls.Gameplay.HoldGadget.performed += _ =>
        {
            isHoldingGadget = true;
            timeSlow.EnableSlowTime();
            ShowGadgetWheel(true);
        };
        controls.Gameplay.HoldGadget.canceled += _ =>
        {
            isHoldingGadget = false;
            timeSlow.DisableSlowTime();
            ShowGadgetWheel(false);
        };
        controls.Gameplay.Gadget1.performed += _ => { if (isHoldingGadget) ActivateGadget(0); };
        controls.Gameplay.Gadget2.performed += _ => { if (isHoldingGadget) ActivateGadget(1); };
    }

    private void OnEnable() => controls.Gameplay.Enable();
    private void OnDisable() => controls.Gameplay.Disable();

    void Fire(Hand hand)
    {
        Debug.Log($"Fire {hand}");
        // WeaponController.Fire(hand);
    }

    private void OnNext()
    {
        inventory.SwitchToNextWeapon();
        switcher.EquipCurrentWeapon(controls);
    }

    private void OnPrev()
    {
        inventory.SwitchToPrevWeapon();
        switcher.EquipCurrentWeapon(controls);
    }
    
    void ShowAbilityWheel(bool show) => Debug.Log("Ability Wheel: " + show);
    void ActivateAbility(int index) => Debug.Log("Activate Ability " + index);
    void ShowGadgetWheel(bool show) => Debug.Log("Gadget Wheel: " + show);
    void ActivateGadget(int index) => Debug.Log("Activate Gadget " + index);
}

public enum Hand { Left, Right }
