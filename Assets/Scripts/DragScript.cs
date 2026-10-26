
using UnityEngine;
using UnityEngine.EventSystems;

public class DragScript : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    public Vector3 offset = new Vector3(-5f, 2f, 0f);
    public RectTransform grid;

    public void OnBeginDrag(PointerEventData eventData)
    {

    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = (Vector3)eventData.position + offset;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Snap();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            //Debug.Log("Rotate");

            Piece piece = GetComponent<Piece>();

            piece.turnClockwise(90);
        }

    }

    public void Snap()
    {
        Tile[] tiles = grid.GetComponentsInChildren<Tile>();

        Tile closest = null;
        float distance = Mathf.Infinity;

        foreach (Tile tile in tiles)
        {
            float d = Vector3.Distance(transform.position, tile.transform.position);

            if (d < distance)
            {
                distance = d;
                closest = tile;
            }
        }

        if (closest != null)
            transform.position = closest.transform.position;
    }
}
