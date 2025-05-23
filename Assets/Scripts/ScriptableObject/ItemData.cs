using UnityEngine;

public enum ItemEffectType
{
    
    Heal,         // ü�� ȸ��
    SpeedBoost    // �̵� �ӵ� ����
}

[CreateAssetMenu(fileName = "ItemData", menuName = "New Item", order = 1)]
public class ItemData : ScriptableObject
{
    [Header("�⺻ ����")]
    public string itemName;
    public string description;
    public Sprite Icon;
    public GameObject worldPrefab;

    [Header("ȿ��")]
    public ItemEffectType effectType;
    public float value;      // ��: ȸ����, �ӵ� ������
    public float duration;   // ���� �ð� (0�̸� ��� ����)


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
