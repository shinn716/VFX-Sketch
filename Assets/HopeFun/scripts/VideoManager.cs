using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{
    public class Config
    {
        public string VidepUrl;
    }

    [SerializeField] VideoPlayer videoPlayer;

    private Config config;

    private void Awake()
    {
        LoadConfig();
    }

    void Start()
    {
        videoPlayer.url = config.VidepUrl;
        videoPlayer.Play();
    }

    void LoadConfig()
    {
        DirectoryInfo dir = new DirectoryInfo(Application.streamingAssetsPath);
        FileInfo[] info = dir.GetFiles("*.*");
        foreach(var i in info)
        {
            //print(i.Name);
            if (i.Name.Equals("config.json"))
            {
                var txt = File.ReadAllText(i.FullName);
                config = JsonUtility.FromJson<Config>(txt);
            }
        }
    }
}
