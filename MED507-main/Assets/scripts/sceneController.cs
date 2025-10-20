using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneController : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);   
    }
    void Update()
    {
        // Press the space key to start coroutine
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Use a coroutine to load the Scene in the background
            StartCoroutine(LoadYourAsyncScene());
        }
    }

    IEnumerator LoadYourAsyncScene()
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        int cScene = SceneManager.GetActiveScene().buildIndex;
        Debug.Log(cScene);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(cScene+1);

        asyncLoad.allowSceneActivation = false;

        Debug.Log("scene now" + cScene);
        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            if (asyncLoad.progress >= 0.9f)
            {
                Debug.Log("scene is loaded");
                if (Input.GetKeyDown (KeyCode.Space)) 
                {
                    asyncLoad.allowSceneActivation = true;
                }
            }
            yield return null;
        }
    }
}
