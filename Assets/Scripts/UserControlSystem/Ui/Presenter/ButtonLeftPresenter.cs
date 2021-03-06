using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonLeftPresenter : MonoBehaviour
{
    [SerializeField] private  Image _selectedImage;
    [SerializeField] private Slider _heathSlider;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Image _sliderbackground;
    [SerializeField] private Image _sliderFillmage;
    [SerializeField] private SelectableValue _selectableValue;


    private void Start()
    {

        _selectableValue.OnSelected += onSelected;
        onSelected(_selectableValue.CurrentValue);
    }

    private void onSelected(ISelectable selected)
    {
        _selectedImage.enabled = selected != null;
        _heathSlider.gameObject.SetActive(selected !=null);
        _text.enabled = selected != null;

        if (selected!=null)
        {
            _selectedImage.sprite = selected.icon;
            _text.text = $"{selected.Health}/{selected.MaxHealth}";
            _heathSlider.minValue = 0;
            _heathSlider.maxValue = selected.MaxHealth;
            _heathSlider.value = selected.Health;
            var color = Color.Lerp(Color.red, Color.green, selected.Health/(float)selected.MaxHealth);
            _sliderbackground.color = color * 0.5f;
            _sliderFillmage.color = color;

        }

    }
}
