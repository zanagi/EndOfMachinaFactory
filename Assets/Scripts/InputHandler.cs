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

    // Key Input
    // Up, right, down, left
    private bool[] arrowKeyPress = new bool[4];
    private KeyCode[] keyCodes = { KeyCode.UpArrow, KeyCode.RightArrow, KeyCode.DownArrow, KeyCode.LeftArrow };

    public bool IsKeyDown(KeyCode keyCode)
    {
        for(int i = 0; i < keyCodes.Length; i++)
        {
            if (keyCode == keyCodes[i])
                return arrowKeyPress[i];
        }
        return false;
    }

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
		Touched = Input.GetMouseButton(0);
		TouchPosition = Input.mousePosition;

        // Key input
        for (int i = 0; i < arrowKeyPress.Length; i++)
            arrowKeyPress[i] = (arrowKeyPress[i] && !Input.GetKeyUp(keyCodes[i])) || (!arrowKeyPress[i] && Input.GetKeyDown(keyCodes[i]));
    }
}