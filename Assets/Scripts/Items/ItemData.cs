using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Objects/ItemData")]
public class ItemData : ScriptableObject
{
    public enum ItemType
    {
        //Melee,
        //Range,
        DamageUp,
        AttackSpeedUp,
        ProjectileCountUp,
        MoveSpeedUp,
        MaxHPUp,
        Heal,
        OrbitSword, // 회전검 개수 증가
        OrbitSwordDamage, // 회전검 공격력 증가
        HPRegen
    }

    [Header("Main Info")]
    public ItemType _itemType;
    public int _itemId;
    public string _itemName;
    public string _itemInfo; // Item 관련 설명
    public Sprite _itemIcon;

    [Header("Level Data")]
    public float _baseDamage; // 기본 대미지
    public int _baseCount; // 투사체 개수 또는 관통 횟수

    [Header("Weapon")]
    public GameObject _projectile;
}
