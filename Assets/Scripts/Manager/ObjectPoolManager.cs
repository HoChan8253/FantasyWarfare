using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class ObjectPoolManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _prefabs;

    List<GameObject>[] _objPools;

    private void Awake()
    {
        _objPools = new List<GameObject>[_prefabs.Length];

        for(int index = 0; index < _objPools.Length; index++)
        {
            _objPools[index] = new List<GameObject>();
        }
    }

    private GameObject Get(int index)
    {
        GameObject select = null;

        foreach (GameObject _obj in _objPools[index])
        {
            if(!_obj.activeSelf)
            {
                select = _obj;
                select.SetActive(true);
                break;
            }
        }

        if(!select)
        {
            select = Instantiate(_prefabs[index], transform);
            _objPools[index].Add(select);
        }
        return select;
    }

}
