using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hopeFun : MonoBehaviour
{
    public static hopeFun Instance;

    public AudioSource audioSources;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        SceneController.Instance.SendMessage("SetCountDown");
        SceneController.Instance.SendMessage("SetMuteMsg");
    }
}
