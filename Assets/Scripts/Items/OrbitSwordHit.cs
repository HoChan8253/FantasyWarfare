using UnityEngine;

public class OrbitSwordHit : MonoBehaviour
{
    [SerializeField] private int _baseDamage = 3;

    public AudioClip _hitSFX;
    private ItemManager _itemManager;
    float _multi = 1f; 

    public void Init(ItemManager manager) // OrbitWeapon 에서 연결해줄 예정
    {
        _itemManager = manager;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"[OrbitSwordHit] 충돌 : {collision.name}");

        EnemyController enemy = collision.GetComponent<EnemyController>();
        if(enemy == null)
        {
            return;
        }

        if(_itemManager != null)
        {
            _multi *= _itemManager._damageMultiplier;
            _multi *= _itemManager._orbitDamageMultiplier;
        }

        int finalDamage = Mathf.RoundToInt(_baseDamage * _multi);

        enemy.TakeDamage(finalDamage);
        SoundManager._instance.PlaySFX(_hitSFX);
    }
}
