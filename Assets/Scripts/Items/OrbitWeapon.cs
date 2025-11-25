using UnityEngine;
using System.Collections.Generic;

public class OrbitWeapon : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private GameObject _swordPrefab;
    [SerializeField] private float _radius = 1.2f;
    [SerializeField] private float _rotateSpeed = 150f;

    [Header("Current State")]
    [SerializeField] private int _swordCount = 0; // 현재 검 개수

    private readonly List<Transform> _swords = new();

    private void Start()
    {
        RefreshSwords();
    }

    private void Update()
    {
        // 이 스크립트가 붙은 오브젝트 회전
        transform.Rotate(0f, 0f, _rotateSpeed * Time.deltaTime);
    }

    private void RefreshSwords() // 현재 _swordCount 기준으로 검 생성 / 배치
    {
        foreach(var sword in _swords)
        {
            if(sword != null)
            {
                Destroy(sword.gameObject);
            }
        }
        _swords.Clear();

        if(_swordCount <= 0)
        {
            return;
        }

        float angleCalc = 360f / _swordCount;

        for(int i = 0; i < _swordCount; i++)
        {
            float angle = angleCalc * i;
            float rad = angle * Mathf.Deg2Rad;

            Vector3 localPos = new Vector3(
                Mathf.Cos(rad) * _radius,
                Mathf.Sin(rad) * _radius,
                0f
            );

            GameObject swordObj = Instantiate(_swordPrefab, transform);
            swordObj.transform.localPosition = localPos;
            swordObj.transform.localRotation = Quaternion.identity;

            _swords.Add(swordObj.transform);
        }
    }

    public void AddSword(int amount = 1) // 레벨업 옵션에서 회전검 개수 증가시 호출
    {
        _swordCount += amount;
        RefreshSwords();
    }

    public void SetSwordCount(int count) // 외부에서 개수 직접 세팅
    {
        _swordCount = Mathf.Max(0, count);
        RefreshSwords();
    }
}
