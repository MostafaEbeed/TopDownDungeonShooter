using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStatsData", menuName = "Game/Player Stats Data")]
public class PlayerStatsData : ScriptableObject
{
    public float moveSpeed = 5f;
    public float dashSpeed = 10f;
    public float dashDuration = 0.3f;
    public float health = 100f;
    public float defense = 10f;
}