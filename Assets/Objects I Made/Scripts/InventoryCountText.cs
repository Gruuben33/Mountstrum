using UnityEngine;
using TMPro;

public class InventoryCountText : MonoBehaviour
{
    [SerializeField] private PlayerStats PlayerStats;
    private TextMeshProUGUI HookCountText;

    void Awake()
    {
        HookCountText = this.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        HookCountText.text = "Hooks: " + PlayerStats.HookCount;
    }
}
