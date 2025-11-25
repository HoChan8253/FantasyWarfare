using System;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    [SerializeField] private int _maxHP = 100;
    private int _currentHP;

    public int MaxHP => _maxHP;
    public int CurrentHP => _currentHP;

    public event Action OnDie;
    public event Action<int, int> OnHealthChanged; // <currentHP , maxHP>

    private void Awake()
    {
        _currentHP = _maxHP;
    }

    public void TakeDamage(int damage)
    {
        _currentHP -= damage;
        if(_currentHP < 0)
        {
            _currentHP = 0;
        }

        OnHealthChanged?.Invoke(_currentHP, _maxHP);
        Debug.Log($"플레이어 피격! 현재 HP : {_currentHP}");

        if(_currentHP == 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        _currentHP += amount;
        if(_currentHP > _maxHP)
        {
            _currentHP = _maxHP;
        }

        OnHealthChanged?.Invoke(_currentHP, _maxHP);
    }

    private void Die()
    {
        OnDie?.Invoke();
        Debug.Log("Game Over");
    }
}
