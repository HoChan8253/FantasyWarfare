using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _lifeTime = 3f;

    private int _damage;
    private Vector2 _direction;
    private float _elapsed;

    public void Init(Vector2 direction, int damage)
    {
        _direction = direction;
        _damage = damage;
        _elapsed = 0f;
    }

    private void OnEnable()
    {
        _elapsed = 0f;
    }

    private void Update()
    {
        transform.Translate(_direction * _speed * Time.deltaTime);

        _elapsed += Time.deltaTime;
        if(_elapsed >= _lifeTime)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyController enemy = collision.GetComponent<EnemyController>();
        if (enemy != null)
        {
            enemy.TakeDamage(_damage);
            gameObject.SetActive(false);
        }
    }
}
