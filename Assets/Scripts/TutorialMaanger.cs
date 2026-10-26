using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using TMPro;

public class TutorialMaanger : MonoBehaviour//welp i spelt it wrong
{
    public List<string> texts = new List<string>();
    public int number = 0;
    public TextMeshProUGUI stuffText;

    void Update()
    {
        if (Keyboard.current.fKey.wasPressedThisFrame)
        {
            number++;
        }

        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            number--;
        }

        if (number < 0)
        {
            number = 0;
        }

        if (number < texts.Count)
        {
            stuffText.text = texts[number];
        }
        else
        {
            stuffText.text = texts[texts.Count - 1];
            number = texts.Count - 1;
        }
    }
}