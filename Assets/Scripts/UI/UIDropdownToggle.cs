using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class UIDropdownToggle : MonoBehaviour
{
    private Toggle toggle;

    [SerializeField]
    private Sprite downArrow;
    [SerializeField]
    private Sprite rightArrow;

    [SerializeField]
    private Image graphic;



    public void Awake()
    {
        toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(OnToggleValueChanged);

        OnToggleValueChanged(toggle.isOn);
    }
    public void OnToggleValueChanged(bool value)
    {
        graphic.sprite = value ? downArrow : rightArrow;
    }
}
