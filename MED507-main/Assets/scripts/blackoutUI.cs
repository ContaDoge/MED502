using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class blackoutUI : MonoBehaviour
{
    public GameObject blackoutPanel;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            StartCoroutine(FadeBlackoutSquare());
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            StartCoroutine(FadeBlackoutSquare(false));
        }
    }

    public IEnumerator FadeBlackoutSquare(bool fadeToBlack = true, int fadeSpeed = 5)
    {
        Color objectColor = blackoutPanel.GetComponent<Image>().color;
        float fadeAmount;
        if (fadeToBlack)
        {
            while (blackoutPanel.GetComponent<Image>().color.a < 1)
            {
                fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);

                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                blackoutPanel.GetComponent<Image>().color = objectColor;
                yield return null;
            }
        }
        else
        {
            while (blackoutPanel.GetComponent<Image>().color.a > 0)
            {
                fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);
                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                blackoutPanel.GetComponent<Image>().color = objectColor;
                yield return null;
            }
        }
    }
}
