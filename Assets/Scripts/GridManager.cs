using UnityEngine;
using UnityEngine.UI;

public class GridManager : MonoBehaviour
{
    public RectTransform painting;
    public int rows;
    public int columns;
    public float squareSize = 100f;
    public GameObject tilePref;
    public GameObject grid;
    

    void Start()
    {
        GridLayoutGroup layout = grid.GetComponent<GridLayoutGroup>();//setting the tile size
        layout.cellSize = new Vector2(squareSize, squareSize);


        rows = Mathf.FloorToInt(painting.rect.height / squareSize);//math stuff
        columns = Mathf.FloorToInt(painting.rect.width / squareSize);

        for (int i = 0; i < rows * columns; i++)//i prob should have made one for rows and another for cols so i can have row + col num instead of i
        {
            GameObject tile = Instantiate(tilePref, grid.transform);
            tile.name = $"Tile number {i+1}";
            Tile script = tile.GetComponent<Tile>();
            script.tileNum = i+1;
            script.painting = painting;
            script.squareSize = squareSize;
        }
    }
}

public enum PieceType 
{ 
    Straight, 
    L,
    Square,
    Tree,//<- 3 piece
    T,
    Elbow
}