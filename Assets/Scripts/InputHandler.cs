using UnityEngine;
using UnityEngine.EventSystems;

public class InputHandler : MonoBehaviour
{
    public bool Touched { get; private set; }
    public bool PreviousTouch { get; private set; }
    public bool TouchReleased { get { return !Touched && PreviousTouch; } }
    public bool TouchedOverUI
    {
        get { return EventSystem.current.IsPointerOverGameObject(); }
    } 
    public Vector3 TouchPosition { get; private set; }
    public Vector3 PreviousTouchPosition { get; private set; }
    private int fingerId;

    // Swipe
    private float swipeResistance = 50;
    private Vector2 swipeModifier = new Vector2(1.0f, 1.5f);
    public bool Swiped { get; private set; }
    public Vector2 SwipeDirection { get; private set; }

    public static InputHandler Instance { get; private set; }

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Update()
    {
        PreviousTouch = Touched;
        PreviousTouchPosition = TouchPosition;

        // Touch input
		if (Application.isMobilePlatform) {
            HandleMobileTouch();
        } else {
			// Non-mobile
			Touched = Input.GetMouseButton(0);
			TouchPosition = Input.mousePosition;
		}
		CheckSwipe();
    }

    private void HandleMobileTouch()
    {
        if (!Touched)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                if (Input.GetTouch(i).phase == TouchPhase.Began && !TouchedOverUI)
                {
                    Touched = true;
                    fingerId = Input.GetTouch(i).fingerId;
                    TouchPosition = Input.GetTouch(i).position;
                    return;
                }
            }
            return;
        }
        for (int i = 0; i < Input.touchCount; i++)
        {
            if (Input.GetTouch(i).fingerId == fingerId)
            {
                if (Input.GetTouch(i).phase == TouchPhase.Ended)
                    Touched = false;
                else
                    TouchPosition = Input.GetTouch(i).position;
                return;
            }
        }
    }

    private void CheckSwipe()
    {
		if (!Touched && PreviousTouch)
        {
			SwipeDirection = TouchPosition - PreviousTouchPosition;
            Swiped = (Mathf.Abs(SwipeDirection.x * swipeModifier.x) + Mathf.Abs(SwipeDirection.y * swipeModifier.y) >= swipeResistance);
             return;
        }
        Swiped = false;
    }
}