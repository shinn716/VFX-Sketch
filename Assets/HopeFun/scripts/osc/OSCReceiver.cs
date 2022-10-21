using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OSCReceiver : MonoBehaviour
{
    public static OSCReceiver Instance;

    [SerializeField] private OSC oscReference;

    private string msg = string.Empty;
    private SceneController sceneController;


    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        sceneController = GetComponent<SceneController>();
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
                    sceneController.LoadScene(0);
                else if (msg.Equals("scene1"))
                    sceneController.LoadScene(1);
                else if (msg.Equals("scene2"))
                    sceneController.LoadScene(2);
                break;
            case "/app":
                msg = _message.values[0].ToString();
                if (msg.Equals("close"))
                    Application.Quit();
                break;
            case "/countdown":
                msg = _message.values[0].ToString();
                int.TryParse(msg, out int _tmp);
                SceneController.Minutes = _tmp;
                sceneController.SetCountDown(); 
                break;
        }
    }
}
