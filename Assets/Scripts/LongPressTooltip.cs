using UnityEngine;
using UnityEngine.EventSystems;

public class LongPressTooltip : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private float longPressDuration = 0.5f;   
    [SerializeField] private GameObject tooltipObject; 

    private bool isPressing = false;
    private float pressStartTime = 0.0f;

    private void Update()
    {
        if (isPressing && Time.time - pressStartTime >= longPressDuration)
        {
            ShowTooltip();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isPressing = true;
        pressStartTime = Time.time;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPressing = false;
        HideTooltip();
    }

    private void ShowTooltip()
    {
        tooltipObject.SetActive(true);
    }

    private void HideTooltip()
    {
        tooltipObject.SetActive(false);
    }
}
