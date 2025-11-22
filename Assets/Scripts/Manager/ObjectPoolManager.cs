using UnityEngine;
using System.Collections.Generic;

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

    public void ReturnObj(GameObject obj)
    {
        obj.SetActive(false);
    }

}
