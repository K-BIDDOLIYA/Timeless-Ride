using UnityEngine;
using UnityEngine.EventSystems;

// Put this on an on-screen UI Button (one for Accelerate, one for Brake).
// Set inputValue to 1 for accelerate, -1 for brake, and assign the car.
// Uses pointer down/up so holding the button drives continuously —
// works for both touch (mobile) and mouse click (WebGL desktop).
public class TouchButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public CarController carController;
    public float inputValue = 1f;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (carController != null) carController.SetInput(inputValue);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (carController != null) carController.SetInput(0f);
    }
}
