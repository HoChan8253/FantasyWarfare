using UnityEngine;

public class UIWiggle : MonoBehaviour
{
    [Header("Position Wiggle")]
    [SerializeField] private float _moveUpDown = 10f;
    [SerializeField] private float _moveSpeed = 1f;

    [Header("Rotation Wiggle")]
    [SerializeField] private float _rotLeftRight = 3f;
    [SerializeField] private float _rotSpeed = 1.2f;

    private RectTransform _rect;
    private Vector2 _startPos;
    private float _time;

    private void Awake()
    {
        _rect = GetComponent<RectTransform>();
        _startPos = _rect.anchoredPosition;

        if(_rect == null)
        {
            return;
        }
    }

    private void Update()
    {
        _time += Time.deltaTime;

        float offSetY = Mathf.Sin(_time * _moveSpeed) * _moveUpDown;

        float rotZ = Mathf.Sin(_time * _rotSpeed) * _rotLeftRight;

        _rect.anchoredPosition = _startPos + new Vector2(0f, offSetY);
        _rect.localRotation = Quaternion.Euler(0f, 0f, rotZ);
    }
}
