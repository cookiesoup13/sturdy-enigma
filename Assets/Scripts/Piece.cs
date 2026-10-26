using UnityEngine;
using System.Collections.Generic;

public class Piece : MonoBehaviour// this and the otehr tile script was the main 2 ai script that were like 80/20 me ai
{
    public static List<Piece> All = new List<Piece>();

    public PieceType pieceType;
    public int turn;
    public GameObject grid;
    public GameObject box;
    public List<int> requiredTileNumbers = new List<int>();
    public List<Tile> coveredTiles = new List<Tile>();
    public bool isPlaced;
    public float rotationSpeed = 200f;

    Quaternion targetRotation;
    Rigidbody2D rb;
    Vector3 startPos;
    int startTurn;
    bool wasPlaced;
    List<Tile> previousTiles = new List<Tile>();

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        All.Add(this);
    }

    void OnDisable()
    {
        All.Remove(this);
    }

    void Start()
    {
        targetRotation = transform.localRotation;
    }

    void Update()//ok i did the update
    {
        turn = turn % 360;
        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    public void turnClockwise(int degrees)// and these
    {
        turn -= degrees;
        targetRotation = Quaternion.Euler(0, 0, turn);
    }

    public void turnCounterClockwise(int degrees)// and this
    {
        turn += degrees;
        targetRotation = Quaternion.Euler(0, 0, turn);
    }

    public bool IsCorrect()
    {
        if (!isPlaced) return false;

        foreach (int n in requiredTileNumbers)
        {
            bool found = false;
            foreach (Tile t in coveredTiles)
            {
                if (t.tileNum == n) found = true;
            }
            if (!found) return false;
        }

        return true;
    }//teamwork?
    //ai did from here down
    public void OnPickedUp()
    {
        startPos = transform.position;
        startTurn = turn;
        wasPlaced = isPlaced;
        previousTiles = new List<Tile>(coveredTiles);

        isPlaced = false;
        coveredTiles.Clear();

        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.bodyType = RigidbodyType2D.Kinematic;

        foreach (BoxCollider2D col in GetComponentsInChildren<BoxCollider2D>())
            col.enabled = false;
    }

    public void Snap()
    {
        transform.localRotation = targetRotation;

        BoxCollider2D[] cols = GetComponentsInChildren<BoxCollider2D>();
        foreach (BoxCollider2D col in cols)
            col.enabled = true;

        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;

        if (!TouchingBox(cols))
        {
            isPlaced = false;
            coveredTiles.Clear();
            rb.bodyType = RigidbodyType2D.Dynamic;
            return;
        }

        Tile[] tiles = grid.GetComponentsInChildren<Tile>();

        float best = Mathf.Infinity;
        Vector3 bestMove = Vector3.zero;
        List<Tile> bestTiles = null;

        foreach (BoxCollider2D col in cols)
        {
            Vector3 cell = col.transform.TransformPoint(col.offset);

            foreach (Tile tile in tiles)
            {
                Vector3 move = tile.transform.position - cell;
                move.z = 0f;

                if (move.sqrMagnitude < best)
                {
                    List<Tile> result = TryPlace(move, cols, tiles);
                    if (result != null)
                    {
                        best = move.sqrMagnitude;
                        bestMove = move;
                        bestTiles = result;
                    }
                }
            }
        }

        if (bestTiles != null)
        {
            transform.position += bestMove;
            coveredTiles = bestTiles;
            isPlaced = true;
            rb.bodyType = RigidbodyType2D.Kinematic;
        }
        else
        {
            turn = startTurn;
            targetRotation = Quaternion.Euler(0, 0, turn);
            transform.localRotation = targetRotation;
            transform.position = startPos;
            coveredTiles = previousTiles;
            isPlaced = wasPlaced;

            if (wasPlaced)
                rb.bodyType = RigidbodyType2D.Kinematic;
            else
                rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }

    bool TouchingBox(BoxCollider2D[] cols)
    {
        if (box == null) return false;

        Collider2D boxCol = box.GetComponent<Collider2D>();
        if (boxCol == null) return false;

        Physics2D.SyncTransforms();

        foreach (BoxCollider2D col in cols)
        {
            if (col.bounds.Intersects(boxCol.bounds)) return true;
        }

        return false;
    }

    List<Tile> TryPlace(Vector3 move, BoxCollider2D[] cols, Tile[] tiles)
    {
        List<Tile> result = new List<Tile>();

        foreach (BoxCollider2D col in cols)
        {
            Vector3 cell = col.transform.TransformPoint(col.offset) + move;
            float tolerance = Mathf.Abs(col.size.x * col.transform.lossyScale.x) * 0.25f;

            Tile found = null;
            foreach (Tile tile in tiles)
            {
                if (Vector2.Distance(tile.transform.position, cell) < tolerance)
                {
                    found = tile;
                    break;
                }
            }

            if (found == null) return null;
            if (result.Contains(found)) return null;

            foreach (Piece p in All)
            {
                if (p != this && p.isPlaced && p.coveredTiles.Contains(found)) return null;
            }

            result.Add(found);
        }

        return result;
    }
}