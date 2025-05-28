using TMPro;
using Unity.VisualScripting;
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
    [SerializeField] private TMP_Text labelText;
    
    private float _gridItemSize;
    private int _gridIndex = 0;
    private int _gridMaxIndex = 1;
    
    // states
    private ScrollStates _scrollState = ScrollStates.Idle;
    
    private void Start()
    {
        _gridMaxIndex = gameObject.transform.childCount - 1;

        if (horizontalGrid != null)
        {
            _gridItemSize = horizontalGrid.rect.width + gameObject.GetComponent<HorizontalLayoutGroup>().spacing;
        }
        
        labelText.text = horizontalGrid.GetChild(_gridIndex).name;
    }

    private void Update()
    {
        if (Keyboard.current.leftArrowKey.wasPressedThisFrame)
        {
            MoveBy(1);
        }

        if (Keyboard.current.rightArrowKey.wasPressedThisFrame)
        {
            MoveBy(-1);
        }
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
        if (units == 0) return;
        
        _gridIndex -= units;
        
        // snap to other side
        if (_gridIndex < 0)
        {
            _gridIndex = _gridMaxIndex + _gridIndex + 1;
        }
        if (_gridIndex > _gridMaxIndex)
        {
            _gridIndex = _gridIndex - _gridMaxIndex - 1;
        }
        
        // move grid
        var vector2 = horizontalGrid.anchoredPosition;
        vector2.x = -_gridIndex * _gridItemSize;
        horizontalGrid.anchoredPosition = vector2;
        
        // change name label by getting child name
        labelText.text = horizontalGrid.GetChild(_gridIndex).name;
    }
}
