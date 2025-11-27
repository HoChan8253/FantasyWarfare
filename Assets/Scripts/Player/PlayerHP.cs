using System;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    [SerializeField] private int _maxHP = 100;
    private int _currentHP;
    private bool _isDead;

    public int MaxHP => _maxHP;
    public int CurrentHP => _currentHP;

    public event Action OnDie;
    public event Action<int, int> OnHealthChanged; // <currentHP , maxHP>

    private Animator _anim;
    private PlayerController _controller;
    private PlayerAttackController _attack;

    private void Awake()
    {
        _currentHP = _maxHP;

        _anim = GetComponent<Animator>();
        _controller = GetComponent<PlayerController>();
        _attack = GetComponent<PlayerAttackController>();
    }

    public void TakeDamage(int damage)
    {
        if(_isDead)
        {
            return;
        }

        _currentHP -= damage;
        if(_currentHP < 0)
        {
            _currentHP = 0;
        }

        OnHealthChanged?.Invoke(_currentHP, _maxHP);
        //Debug.Log($"플레이어 피격! 현재 HP : {_currentHP}");

        if(_currentHP == 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        if(_isDead)
        {
            return;
        }

        _currentHP += amount;
        if(_currentHP > _maxHP)
        {
            _currentHP = _maxHP;
        }
        OnHealthChanged?.Invoke(_currentHP, _maxHP);
    }

    private void Die()
    {
        if(_isDead)
        {
            return;
        }

        _isDead = true;

        if(_controller != null)
        {
            _controller.enabled = false;
        }

        if (_attack != null)
        {
            _attack.enabled = false;
        }

        if(_anim != null)
        {
            _anim.SetTrigger("Die");
        }

        GameManager._instance.GameOver();

        OnDie?.Invoke();
        //Debug.Log("Game Over");
    }
}
