using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private PlayerStatsData baseStats;

    public float MoveSpeed { get; private set; }
    public float DashSpeed { get; private set; }
    public float DashDuration { get; private set; }
    public float Health { get; private set; }
    public float Defense { get; private set; }

    private void Awake()
    {
        // Cache base values
        MoveSpeed = baseStats.moveSpeed;
        DashSpeed = baseStats.dashSpeed;
        DashDuration = baseStats.dashDuration;
        Health = baseStats.health;
        Defense = baseStats.defense;
    }

    // Optionally expose runtime modifiers:
    public void ModifySpeed(float multiplier) => MoveSpeed *= multiplier;
    public void TakeDamage(float amount) => Health -= Mathf.Max(0, amount - Defense);
}