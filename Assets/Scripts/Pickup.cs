using UnityEngine;
using UnityEngine.InputSystem;

public class Pickup : MonoBehaviour
{
    public RectTransform point;
    public GameObject heldPiece;
    public GameObject hint;
    public GameObject paint;
    public bool holding = false;

    Vector3 offset;

    void Update()
    {
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            if (!holding)//not holding -> trying to grab
            {
                Collider2D hit = Physics2D.OverlapPoint(point.position);//i still have no idea how this workss

                if (hit != null && hit.CompareTag("Piece"))
                {

                    heldPiece = hit.transform.parent.gameObject;

                    offset = heldPiece.transform.position - hit.bounds.center;
                    offset.z = 0f;

                    heldPiece.GetComponent<Piece>().OnPickedUp();
                    holding = true;
                }
            }
            else//grabing -> trying to release
            {
                heldPiece.GetComponent<Piece>().Snap();//snappers
                heldPiece = null;
                holding = false;
            }
        }

        if (holding && heldPiece != null)//actuallyr hold smthn
        {
            Vector3 pos = point.position + offset;
            pos.z = heldPiece.transform.position.z;
            heldPiece.transform.position = pos;
        }

        if (Keyboard.current.qKey.wasPressedThisFrame && heldPiece != null)//rotat
        {
            heldPiece.GetComponent<Piece>().turnClockwise(90);//i wish there could be someway to roate the otehr way
        }

        if (Mouse.current.rightButton.isPressed)
        {
            paint.SetActive(true);
        }
        else
        {
            paint.SetActive(false);
        }

        if (Mouse.current.leftButton.isPressed)
        {
            hint.SetActive(true);
        }
        else
        {
            hint.SetActive(false);
        }
    }
}