using Unity.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : ScriptableObject
{
    [Header("Basic Info")]
    public string _enemyName;

    [Header("Replace")]
    [Tooltip("이 거리보다 멀어지면 플레이어 근처로 재배치")]
    public float _repoDistance = 10f;

    [Tooltip("플레이어 주변 재배치")]
    public float _respawnRadius = 8f;

    [Header("Combat Status")]
    public int _maxHP = 10;
    public float _moveSpeed = 2f;
    public int _damage = 1;
    public int _exp = 5;

    [Header("Visual")]
    public Sprite _sprite;
    public RuntimeAnimatorController _animatorController;

    [Header("Sound")]
    public AudioClip deathSFX;
}
