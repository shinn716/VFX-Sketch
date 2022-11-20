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
    public static SceneController Instance;

    public static int Minutes = 10;
    public static bool Mute = false;

    [SerializeField] Toggle togMute;
    [SerializeField] Image imgConnection;
    [SerializeField] Text txtRemoteIp;
    [SerializeField] Camera maincam;
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] Countdown countdown = null;
    [SerializeField] AudioListener audioListener = null;

    private int currentScene = 0;
    private bool loadFlag = false;
    private StringBuilder sb = new StringBuilder();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        var str = Utils.GetLocalIP();
        sb.AppendLine("Local IP:");

        OSCReceiver.Instance.isConnection += Connection;
        OSCReceiver.Instance.selectScene += LoadScene;
        OSCReceiver.Instance.setMinute += SetCountDown;
        OSCReceiver.Instance.setMute += SetMute;
        OSCReceiver.Instance.isQuit += Quit;

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
    private void OnDestroy()
    {
        OSCReceiver.Instance.isConnection -= Connection;
        OSCReceiver.Instance.selectScene -= LoadScene;
        OSCReceiver.Instance.setMinute -= SetCountDown;
        OSCReceiver.Instance.setMute -= SetMute;
        OSCReceiver.Instance.isQuit -= Quit;
    }


    public void SetMinutes(string _input)
    {
        int.TryParse(_input, out int value);
        Minutes = value;
    }



    public void SetMute(bool _mute)
    {
        Mute = _mute;
        StartCoroutine(SetMuteCo(Mute));
    }
    private void SetMuteMsg()
    {
        StartCoroutine(SetMuteCo(Mute));
    }

    private IEnumerator SetMuteCo(bool _mute)
    {
        if (audioListener == null)
            audioListener = FindObjectOfType<AudioListener>();
        yield return null;

        if (audioListener == null)
            yield break;
        audioListener.enabled = !_mute;
        togMute.isOn = _mute;
    }


    private void Quit()
    {
        Application.Quit();
    }
    private void SetCountDown()
    {
        if (countdown == null)
            countdown = FindObjectOfType<Countdown>();
        countdown?.SetValue(Minutes);
    }
    private void SetCountDown(int _minutes)
    {
        Minutes = _minutes;
        if (countdown == null)
            countdown = FindObjectOfType<Countdown>();
        countdown?.SetValue(Minutes);
    }
    private void LoadScene(int _index)
    {
        switch (_index)
        {
            case 0:
                if (loadFlag)
                    return;
                canvasGroup.alpha = 1;
                maincam.enabled = true;
                UnloadAllScenesExcept("0_Controller");
                //DisposeScene(currentScene);
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
    private void DisposeScene(int _scene)
    {
        if (_scene == 1)
            SceneManager.UnloadSceneAsync("1_HopeFun_0");
        else if (_scene == 2)
            SceneManager.UnloadSceneAsync("2_HopeFun_1");
    }
    private void UnloadAllScenesExcept(string sceneName)
    {
        int c = SceneManager.sceneCount;
        for (int i = 0; i < c; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            if (scene.name != sceneName)
            {
                SceneManager.UnloadSceneAsync(scene);
            }
        }
    }
    private void Connection()
    {
        StartCoroutine(ConnectionCo());
    }
    private IEnumerator ConnectionCo()
    {
        imgConnection.color = Color.green;
        yield return new WaitForSeconds(3);
        imgConnection.color = Color.clear;
    }
    private IEnumerator LoadAsyncScene(string _name)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(_name, LoadSceneMode.Additive);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        loadFlag = false;
    }
}
