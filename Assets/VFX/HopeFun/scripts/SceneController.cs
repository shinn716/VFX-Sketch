using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] Camera maincam;
    [SerializeField] CanvasGroup canvasGroup;

    private int currentScene = 0;
    private bool loadFlag = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && !loadFlag)
        {
            canvasGroup.alpha = 0;
            maincam.enabled = false;
            loadFlag = true;
            DisposeScene(currentScene);
            StartCoroutine(LoadAsyncScene("1_HopeFun_0"));
            //SceneManager.LoadSceneAsync("1_HopeFun_0", LoadSceneMode.Additive);
            currentScene = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && !loadFlag)
        {
            canvasGroup.alpha = 0;
            maincam.enabled = false;
            loadFlag = true;
            DisposeScene(currentScene);
            StartCoroutine(LoadAsyncScene("2_HopeFun_1"));
            //SceneManager.LoadSceneAsync("2_HopeFun_1", LoadSceneMode.Additive);
            currentScene = 2;
        }
        else if (Input.GetKeyDown(KeyCode.BackQuote))
        {
            canvasGroup.alpha = 1;
            maincam.enabled = true;
            DisposeScene(currentScene);
            currentScene = 0;
        }
    }

    void DisposeScene(int _scene)
    {
        if (_scene == 1)
            SceneManager.UnloadSceneAsync("1_HopeFun_0");
        else if (_scene == 2)
            SceneManager.UnloadSceneAsync("2_HopeFun_1");
    }

    IEnumerator LoadAsyncScene(string _name)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(_name, LoadSceneMode.Additive);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
            loadFlag = false;
        }
    }
}
