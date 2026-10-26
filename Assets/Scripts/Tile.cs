using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour//i not gonna lie ai made most of this cuz i have no idea what any of it does
{
    public RectTransform painting;
    public int tileNum;

    public float squareSize;

    void Start()//I think it calculates the bounds of the tile then uses the read write thing to check what the painting is and gets average somehow
    {
        Image paintingImage = painting.GetComponent<Image>();
        Texture2D texture = paintingImage.sprite.texture;

        int columns = Mathf.FloorToInt(painting.rect.width / squareSize);
        int rows = Mathf.FloorToInt(painting.rect.height / squareSize);

        int index = transform.GetSiblingIndex();
        int col = index % columns;
        int row = index / columns;

        int x0 = col * texture.width / columns;
        int x1 = (col + 1) * texture.width / columns;
        int y0 = (rows - 1 - row) * texture.height / rows;
        int y1 = (rows - row) * texture.height / rows;

        Color32[] pixels = texture.GetPixels32();

        long r = 0, g = 0, b = 0;
        int count = 0;

        for (int y = y0; y < y1; y++)
        {
            for (int x = x0; x < x1; x++)
            {
                Color32 pixel = pixels[y * texture.width + x];
                r += pixel.r;
                g += pixel.g;
                b += pixel.b;
                count++;
            }
        }

        GetComponent<Image>().color = new Color((float)r / count / 255f,(float)g / count / 255f,(float)b / count / 255f,1f);
    }
}