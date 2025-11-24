using UnityEngine;
using System.Collections.Generic;

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager Instance { get; private set; }

    [SerializeField] private int _defaultCapacity = 20;
    [SerializeField] private GameObject[] _prefabs;

    List<GameObject>[] _objPools;
    
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;

        _objPools = new List<GameObject>[_prefabs.Length];

        for(int i = 0; i < _objPools.Length; i++)
        {
            _objPools[i] = new List<GameObject>();

            for(int j = 0; j < _defaultCapacity; j++)
            {
                GameObject obj = Instantiate(_prefabs[i], transform);
                obj.SetActive(false);
                _objPools[i].Add(obj);
            }
        }
    }

    public GameObject GetPoolObj(int index)
    {
        // 잘못된 index 체크
        if(index < 0 || index >= _objPools.Length)
        {
            return null;
        }

        GameObject select = null;

        // 비활성 객체 찾기
        foreach (GameObject _obj in _objPools[index])
        {
            if(!_obj.activeSelf)
            {
                select = _obj;
                //select.SetActive(true);
                break;
            }
        }

        if(select == null)
        {
            select = Instantiate(_prefabs[index], transform);
            _objPools[index].Add(select);
        }
        select.SetActive(true);

        return select;
    }

    public T GetPoolObj<T>(int index) where T : Component
    {
        GameObject obj = GetPoolObj(index);
        if(obj == null)
        {
            return null;
        }

        return obj.GetComponent<T>();
    }

    public void ReturnObj(GameObject obj)
    {
        obj.SetActive(false);
    }

}
