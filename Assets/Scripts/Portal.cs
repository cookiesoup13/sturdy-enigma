using UnityEngine;

public class Portal : MonoBehaviour
{
    public bool atUp = true;

    public RectTransform switchTrans;
    public RectTransform player;
    public RectTransform portal;

    public float switchUpY = -543.43f;
    public float switchDownY = 547.8f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;//ye stuff returnger

        Vector2 switchPos = switchTrans.anchoredPosition;//not pos cuz anchoring suf
        Vector2 playerPos = player.anchoredPosition; //same
        Vector2 portalPos = portal.anchoredPosition; //same

        if (atUp)//going down
        {
            atUp = false;
            switchPos.y = switchDownY;
            portalPos.x = 840f;
        }
        else//going up
        {
            atUp = true;
            switchPos.y = switchUpY;
            portalPos.x = -840f;
        }

        playerPos.x = 605f;
        

        switchTrans.anchoredPosition = switchPos;
        player.anchoredPosition = playerPos;
        portal.anchoredPosition = portalPos;
    }
}