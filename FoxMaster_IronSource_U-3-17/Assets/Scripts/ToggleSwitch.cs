using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class ToggleSwitch : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private bool _isOn = false;
    public bool isOn 
    {get {return _isOn;}}

    [SerializeField] private RectTransform toggleIndicator;
    [SerializeField] private Image backgroundImage;

    private float offX;
    private float onX;

    [SerializeField] private float tweenTime = 0.25f;

    public delegate void ValueChanged(bool value);
    public event ValueChanged valueChanged;

    void Start()
    {
        offX = toggleIndicator.anchoredPosition.x;
        onX = backgroundImage.rectTransform.rect.width - toggleIndicator.rect.width;
    }

    private void OnEnable()
    {
        Toggle(isOn);
    }
    private void Toggle(bool value)
    {
        if(value != isOn)
        {
            _isOn = value;

            MoveIndicator(isOn);

            if(valueChanged != null)
            {
                valueChanged(isOn);
            }
        }
    }
    private void MoveIndicator(bool value)
    {
        if (value)
        {
            toggleIndicator.DOAnchorPosX(onX, tweenTime);
            Debug.Log("Sound Off");

        }
        else
        {
            toggleIndicator.DOAnchorPosX(offX, tweenTime);
            Debug.Log("Sound On");

        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Toggle(!isOn);

    }
}
