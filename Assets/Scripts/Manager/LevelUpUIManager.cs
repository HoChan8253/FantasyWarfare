using Unity.VisualScripting;
using UnityEngine;

public class LevelUpUIManager : MonoBehaviour
{
    [Header("All ItemData List")]
    public ItemData[] _allItem;

    [Header("UI")]
    public GameObject _panel;
    public ItemSlotUI[] _slots;

    private ItemManager _itemManager;
    private PlayerExp _playerExp;

    private void Start()
    {
        Init();

        _panel.SetActive(false);
        _itemManager = GameManager._instance._player.GetComponent<ItemManager>();

        GameManager._instance._playerExp.OnLevelUp += OnLevelUp;
    }

    private void Init()
    {
        if(GameManager._instance == null)
        {
            return;
        }

        if(GameManager._instance._playerExp == null)
        {
            return;
        }
        _playerExp = GameManager._instance._playerExp;
    }

    private void OnLevelUp(int level)
    {
        ShowLevelUpUI();
    }

    private void ShowLevelUpUI()
    {
        _panel.SetActive(true);
        Time.timeScale = 0f; // 일시정지

        ItemData[] randomItems = GetRandomItems(_slots.Length);

        for(int i = 0; i < _slots.Length; i++)
        {
            _slots[i].Setup(randomItems[i], OnSelectItem);
        }
    }

    private ItemData[] GetRandomItems(int count)
    {
        ItemData[] result = new ItemData[count];
        for(int i = 0; i < count; i++)
        {
            int idx = Random.Range(0, _allItem.Length);
            result[i] = _allItem[idx];
        }
        return result;
    }

    private void OnSelectItem(ItemData item)
    {
        _itemManager.ApplyItem(item);

        _panel.SetActive(false);
        Time.timeScale = 1f;
    }
}
