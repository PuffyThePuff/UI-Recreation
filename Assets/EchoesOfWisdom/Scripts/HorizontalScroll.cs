using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;

enum ScrollStates
{
    Idle,
    Scrolling,
    ScrollingLeft,
    ScrollingRight
}

public class HorizontalScroll : MonoBehaviour
{
    [SerializeField] private RectTransform horizontalGrid;
    
    private float _gridItemSize;
    private int _gridIndex = 0;
    private int _gridCount;
    
    // states
    private ScrollStates _scrollState = ScrollStates.Idle;
    
    private void Start()
    {
        _gridCount = gameObject.transform.childCount;

        if (horizontalGrid != null)
        {
            _gridItemSize = horizontalGrid.rect.width + gameObject.GetComponent<HorizontalLayoutGroup>().spacing;
        }
        
        MoveBy(-1);
    }

    public void ScrollPress(InputAction.CallbackContext context)
    {
        Vector2 direction = context.ReadValue<Vector2>();

        if (_scrollState != ScrollStates.Idle) return;
        if (!context.performed) return;
        
        switch (direction.x)
        {
            // left
            case < 0:
                MoveBy(1);
                break;
            // right
            case > 0:
                MoveBy(-1);
                break;
        }
    }

    // -units moves to the right
    // +units moves to the left
    private void MoveBy(int units)
    {
        var vector2 = horizontalGrid.anchoredPosition;
        vector2.x = vector2.x + units * _gridItemSize;
        horizontalGrid.anchoredPosition = vector2;
    }
}
