using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static int Minutes = 10;

    [SerializeField] Camera maincam;
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] Countdown countdown = null;

    private int currentScene = 0;
    private bool loadFlag = false;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && !loadFlag)
            LoadScene(1);
        else if (Input.GetKeyDown(KeyCode.E) && !loadFlag)
            LoadScene(2);
        else if (Input.GetKeyDown(KeyCode.Q))
            LoadScene(0);
    }


    public void LoadScene(int _index)
    {
        switch (_index)
        {
            case 0:
                canvasGroup.alpha = 1;
                maincam.enabled = true;
                DisposeScene(currentScene);
                currentScene = 0;
                break;
            case 1:
                canvasGroup.alpha = 0;
                maincam.enabled = false;
                loadFlag = true;
                DisposeScene(currentScene);
                StartCoroutine(LoadAsyncScene("1_HopeFun_0"));
                currentScene = 1;
                break;
            case 2:
                canvasGroup.alpha = 0;
                maincam.enabled = false;
                loadFlag = true;
                DisposeScene(currentScene);
                StartCoroutine(LoadAsyncScene("2_HopeFun_1"));
                currentScene = 2;
                break;
        }
    }
    public void SetMinutes(string _input)
    {
        int.TryParse(_input, out int value);
        Minutes = value;
    }
    public void SetCountDown()
    {
        if (countdown == null)
            countdown = FindObjectOfType<Countdown>();
        countdown?.SetValue(Minutes);
    }


    private void DisposeScene(int _scene)
    {
        if (_scene == 1)
            SceneManager.UnloadSceneAsync("1_HopeFun_0");
        else if (_scene == 2)
            SceneManager.UnloadSceneAsync("2_HopeFun_1");
    }
    private IEnumerator LoadAsyncScene(string _name)
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
