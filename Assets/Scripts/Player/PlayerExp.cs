using UnityEngine;
using System;

public class PlayerExp : MonoBehaviour
{
    [Header("Level")]
    [SerializeField] private int _level = 1;

    [Header("Experience")]
    [SerializeField] private int _currentExp = 0;
    [SerializeField] private int _expToNextLevel = 5;
    [SerializeField] private float _expGrowthRate = 1.5f; // 레벨업당 요구량 증가 배율

    public int Level => _level;
    public int CurrentExp => _currentExp;
    public int ExpToNextLevel => _expToNextLevel;

    public event Action<int> OnLevelUp;

    public void GainExp(int amount)
    {
        if(amount < 0)
        {
            return;
        }

        _currentExp += amount;

        while(_currentExp >= _expToNextLevel)
        {
            _currentExp -= _expToNextLevel;
            LevelUp();
        }
    }

    private void LevelUp()
    {
        _level++;

        _expToNextLevel = Mathf.RoundToInt(_expToNextLevel * _expGrowthRate);

        Debug.Log($"Level Up! Current Level : {_level}");

        OnLevelUp?.Invoke(_level);
    }
}
