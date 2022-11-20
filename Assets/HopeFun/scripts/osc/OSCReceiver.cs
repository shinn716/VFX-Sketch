using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OSCReceiver : MonoBehaviour
{
    public static OSCReceiver Instance;

    [SerializeField] private OSC oscReference;

    public event Action<bool> setMute;
    public event Action<int> selectScene;
    public event Action<int> setMinute;
    public event Action isConnection;
    public event Action isQuit;

    private string msg = string.Empty;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        oscReference.SetAllMessageHandler(OnReceive);
    }

    private void OnReceive(OscMessage _message)
    {
        print("OnReceive: " + _message.address + " Value: " + _message.values[0].ToString());
        switch (_message.address)
        {
            case "/scene":
                msg = _message.values[0].ToString();
                if (msg.Equals("ready"))
                    selectScene?.Invoke(0);
                else if (msg.Equals("scene1"))
                    selectScene?.Invoke(1);
                else if (msg.Equals("scene2"))
                    selectScene?.Invoke(2);
                break;
            case "/app":
                msg = _message.values[0].ToString();
                if (msg.Equals("close"))
                    isQuit?.Invoke();
                break;
            case "/countdown":
                msg = _message.values[0].ToString();
                int.TryParse(msg, out int _tmp);
                setMinute?.Invoke(_tmp);
                break;
            case "/test":
                isConnection?.Invoke();
                break;
            case "/mute":
                msg = _message.values[0].ToString();
                bool.TryParse(msg, out bool _mute);
                setMute?.Invoke(_mute);
                break;
                
        }
    }
}
