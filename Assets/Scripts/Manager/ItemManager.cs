using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public float _damageMultiplier = 1f;
    public float _attackSpeedMultiplier = 1f;
    public int _projectileCount = 1;
    public float _moveSpeedMultiplier = 1f;
    public int _maxHPBonus = 0;

    private PlayerHP _playerHP;
    private PlayerAttackController _attack;

    private void Awake()
    {
        _playerHP = GetComponent<PlayerHP>();
        _attack = GetComponent<PlayerAttackController>();
    }

    public void ApplyItem(ItemData item)
    {
        switch (item._itemType)
        {
            case ItemData.ItemType.DamageUp:
                _damageMultiplier += item._baseDamage;
                break;

            case ItemData.ItemType.AttackSpeedUp:
                _attackSpeedMultiplier += item._baseDamage;
                break;

            case ItemData.ItemType.ProjectileCountUp:
                _projectileCount += item._baseCount;
                break;

            case ItemData.ItemType.MoveSpeedUp:
                _moveSpeedMultiplier += item._baseDamage;
                break;

            case ItemData.ItemType.MaxHPUp:
                _maxHPBonus += item._baseCount;
                break;

            case ItemData.ItemType.Heal:
                _playerHP.Heal(item._baseCount);
                break;
        }
    }
}
