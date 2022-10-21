using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Text;

public class Utils
{
    public static string[] GetLocalIP(System.Net.Sockets.AddressFamily _type = System.Net.Sockets.AddressFamily.InterNetwork)
    {
        var host = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());
        List<string> returnvalue = new List<string>();
        foreach (var i in host.AddressList)
            if (i.AddressFamily == _type)
                if (i != null)
                    returnvalue.Add(i.ToString());
        return returnvalue.ToArray();
        throw new Exception("No network adapters with an IPv4 address in the system!");
    }
}

public class SceneController : MonoBehaviour
{
    public static int Minutes = 10;

    [SerializeField] Text txtRemoteIp;
    [SerializeField] Camera maincam;
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] Countdown countdown = null;

    private int currentScene = 0;
    private bool loadFlag = false;
    private StringBuilder sb = new StringBuilder();

    private void Start()
    {
        var str = Utils.GetLocalIP();
        sb.AppendLine("Local IP:");
        foreach (var i in str)
            sb.AppendLine(i);
        txtRemoteIp.text = sb.ToString();
    }


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
                if (loadFlag)
                    return;
                canvasGroup.alpha = 1;
                maincam.enabled = true;
                DisposeScene(currentScene);
                currentScene = 0;
                break;
            case 1:
                if (loadFlag)
                    return;
                loadFlag = true;
                canvasGroup.alpha = 0;
                maincam.enabled = false;
                DisposeScene(currentScene);
                StartCoroutine(LoadAsyncScene("1_HopeFun_0"));
                currentScene = 1;
                break;
            case 2:
                if (loadFlag)
                    return;
                loadFlag = true;
                canvasGroup.alpha = 0;
                maincam.enabled = false;
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
        while (!asyncLoad.isDone)
        {
            if (asyncLoad.progress >= 0.9f)
            {
                loadFlag = false;
            }
            yield return null;
        }
    }
}
