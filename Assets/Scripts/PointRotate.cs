using UnityEngine;
using UnityEngine.InputSystem;

public class UIRotator : MonoBehaviour
{
    public RectTransform pivot;
    public RectTransform point;

    public float radius = 100f;
    public float minRad = 30f;   
    public float maxRad = 200f;//i should decrease this later(go to inspector)  

    public float scrollSpeed = 0.5f;//mouse go brr

    void Update()
    {
        
        float scroll = Mouse.current.scroll.ReadValue().y;//mouse value(but for y this time)
        if (scroll != 0f)//scrolller
        {
            radius += Mathf.Sign(scroll) * scrollSpeed * 10f;
            radius = Mathf.Clamp(radius, minRad, maxRad);
        }

        Vector2 mousePos = Mouse.current.position.ReadValue();//mosue value
        Vector2 pivotScreenPos = RectTransformUtility.WorldToScreenPoint(null, pivot.position);//finds the mouse location
        Vector2 direction = (mousePos - pivotScreenPos).normalized;//direction distance things

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;//math again
        point.rotation = Quaternion.Euler(0, 0, angle);

        point.anchoredPosition = pivot.anchoredPosition + (direction * radius);
    }
}