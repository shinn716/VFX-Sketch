using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hopeFun : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SceneController.Instance.SendMessage("SetCountDown");
        SceneController.Instance.SendMessage("SetMuteMsg");


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
