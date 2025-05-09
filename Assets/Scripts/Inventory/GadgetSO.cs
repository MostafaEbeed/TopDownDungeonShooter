using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "NewGadget", menuName = "Inventory/Gadget")]
public class GadgetSO : BaseItemSO
{
    [BoxGroup("Gadget Data")]
    [Required]
    public GameObject gadgetPrefab;

    [BoxGroup("Gadget Stats")]
    [InlineProperty, HideLabel]
    public GadgetStats gadgetStats;
}


[System.Serializable]
public class GadgetStats
{
    public float cooldown;
    public string description;
}