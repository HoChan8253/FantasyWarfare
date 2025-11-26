using Unity.VisualScripting;
using UnityEditor.UIElements;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public float _damageMultiplier = 1f;
    public float _orbitDamageMultiplier = 1f;
    public float _attackSpeedMultiplier = 1f;
    public int _projectileCount = 1;
    public float _moveSpeedMultiplier = 1f;
    public int _maxHPBonus = 0;

    public float _hpRegenPerSecond = 0f;
    private float _regenRemain = 0f; // 소수점 누적용

    private PlayerHP _playerHP;
    private PlayerAttackController _attack;

    [Header("Orbit Weapon")]
    [SerializeField] private OrbitWeapon _orbitWeapon;

    private void Awake()
    {
        _playerHP = GetComponent<PlayerHP>();
        _attack = GetComponent<PlayerAttackController>();
    }

    private void Update()
    {
        if(_hpRegenPerSecond > 0f && _playerHP != null)
        {
            // 초당 회복량
            _regenRemain += _hpRegenPerSecond * Time.deltaTime;

            // 1 이상 모이면 int , Heal 호출
            if(_regenRemain >= 1f)
            {
                int healAmount = Mathf.FloorToInt(_regenRemain);
                _regenRemain -= healAmount;
                _playerHP.Heal(healAmount);
            }
        }
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

            case ItemData.ItemType.OrbitSword:
                ApplyOrbitSword(item);
                break;

            case ItemData.ItemType.OrbitSwordDamage:
                _orbitDamageMultiplier += item._baseDamage;
                break;

            case ItemData.ItemType.HPRegen:
                _hpRegenPerSecond += item._baseDamage;
                break;
        }
    }

    private void ApplyOrbitSword(ItemData item)
    {
        if(!_orbitWeapon.gameObject.activeSelf)
        {
            _orbitWeapon.gameObject.SetActive(true);
        }

        _orbitWeapon.AddSword(item._baseCount);
    }
}
