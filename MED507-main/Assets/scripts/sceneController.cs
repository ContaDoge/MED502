using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class sceneController : MonoBehaviour
{
    public GameObject blackoutPanel;

    //void Awake(){DontDestroyOnLoad(this.gameObject);}

    void Update()
    {
        // Press the space key to start coroutine, for testing purposes

        //Slente her, this way of input use the old input system
        ///if (Input.GetKeyDown(KeyCode.Space))
        /// / therefore will change it to the new one. 
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartLoadNextScene();
        }
    }

    public void StartLoadNextScene()
    {
        StartCoroutine(LoadYourAsyncScene());
    }
    public void fadeToBlack()
    {
        StartCoroutine(FadeBlackoutSquare(true));
    }
    public void fadeFromBlack()
    {
        StartCoroutine(FadeBlackoutSquare(false));
    }

    IEnumerator LoadYourAsyncScene()
    {
        int cScene = SceneManager.GetActiveScene().buildIndex;

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(cScene + 1);
        asyncLoad.allowSceneActivation = false;

        // Wait for the scene to finish loading in background (progress reaches 0.9)
        while (asyncLoad.progress < 0.9f)
        {
            yield return null;
        }

        // Fade to black and wait until fade is complete
        //yield return StartCoroutine(FadeBlackoutSquare(true));

        // Activate the scene
        asyncLoad.allowSceneActivation = true;

        // Wait until the activation completes
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        //// Give the new scene one frame to initialize UI objects
        //yield return new WaitForSeconds(1.5f);

        //// Fade back in (remove the black)
        //yield return StartCoroutine(FadeBlackoutSquare(false));
    }

    public IEnumerator FadeBlackoutSquare(bool fadeToBlack = true, int fadeSpeed = 3)
    {
        if (blackoutPanel == null)
        {
            Debug.LogError("blackoutPanel is not assigned. Assign it in the inspector.");
            yield break;
        }

        Image img = blackoutPanel.GetComponent<Image>();
        if (img == null)
        {
            Debug.LogError("blackoutPanel does not have an Image component.");
            yield break;
        }

        Color color = img.color;
        float targetAlpha = fadeToBlack ? 1f : 0f;

        // Fast-path: if already at target alpha, exit immediately
        if (Mathf.Approximately(color.a, targetAlpha)) yield break;

        while (!Mathf.Approximately(color.a, targetAlpha))
        {
            float delta = fadeSpeed * Time.deltaTime;
            color.a = fadeToBlack ? Mathf.Min(color.a + delta, 1f) : Mathf.Max(color.a - delta, 0f);
            img.color = color;
            yield return null;
        }
    }
}