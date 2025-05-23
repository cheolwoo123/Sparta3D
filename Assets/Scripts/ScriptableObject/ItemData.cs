using UnityEngine;

public enum ItemEffectType
{
    
    Heal,         // 체력 회복
    SpeedBoost    // 이동 속도 증가
}

[CreateAssetMenu(fileName = "ItemData", menuName = "New Item", order = 1)]
public class ItemData : ScriptableObject
{
    [Header("기본 정보")]
    public string itemName;
    public string description;
    public Sprite Icon;
    public GameObject worldPrefab;

    [Header("효과")]
    public ItemEffectType effectType;
    public float value;      // 예: 회복량, 속도 증가량
    public float duration;   // 지속 시간 (0이면 즉시 적용)


    public void Use(Player player)
    {
        switch (effectType)
        {
            case ItemEffectType.Heal:
                player.condition.Heal(value);  
                break;

            case ItemEffectType.SpeedBoost:
                player.StartCoroutine(player.controller.SpeedBoost(value, duration));
                break;
        }
    }


}
