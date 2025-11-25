using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemSlotUI : MonoBehaviour
{
    [SerializeField] private Image _iconImage;
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _infoText;
    [SerializeField] private Button _btn;

    private ItemData _item;
    private System.Action<ItemData> _onSelect;

    public void Setup(ItemData item, System.Action<ItemData> onSelect)
    {
        _item = item;
        _onSelect = onSelect;

        _iconImage.sprite = item._itemIcon;
        _nameText.text = item._itemName;
        _infoText.text = item._itemInfo;

        _btn.onClick.RemoveAllListeners();
        _btn.onClick.AddListener(() =>
        {
            _onSelect?.Invoke(_item);
        });
    }
}
