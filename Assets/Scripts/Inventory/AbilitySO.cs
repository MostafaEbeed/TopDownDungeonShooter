using UnityEngine;
using Sirenix.OdinInspector;

public enum AbilityType { Active, Passive }

[CreateAssetMenu(fileName = "NewAbility", menuName = "Inventory/Ability")]
public class AbilitySO : BaseItemSO
{
    [BoxGroup("Ability Data")]
    [Required]
    public GameObject abilityPrefab;

    [BoxGroup("Ability Stats")]
    [InlineProperty, HideLabel]
    public AbilityStats abilityStats;

    [BoxGroup("Ability Info")]
    [EnumToggleButtons]
    public AbilityType abilityType;
}

[System.Serializable]
public class AbilityStats
{
    public float duration;
    public float cooldown;
    [TextArea]
    public string description;
}