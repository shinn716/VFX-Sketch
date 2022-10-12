using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private int currentScene = 0;

    // Start is called before the first frame update
    void Start()
    {
        //load = SceneManager.LoadSceneAsync("1_HopeFun_0", LoadSceneMode.Additive);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            DisposeScene(currentScene);
            SceneManager.LoadSceneAsync("1_HopeFun_0", LoadSceneMode.Additive);
            currentScene = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            DisposeScene(currentScene);
            SceneManager.LoadSceneAsync("2_HopeFun_1", LoadSceneMode.Additive);
            currentScene = 2;
        }
        else if (Input.GetKeyDown(KeyCode.BackQuote))
        {
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
}
