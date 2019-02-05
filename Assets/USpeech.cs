using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#if PLATFORM_ANDROID
using UnityEngine.Android;
#endif

public class USpeech : MonoBehaviour
{
    public Button m_BtnStart;
    public Button m_BtnStop;
    public Text m_Text;
    public Text m_DebugText;

    private AndroidJavaObject pluginUSpeechInstance;
    private AndroidJavaObject unityContext;
    private Microphone mic = new Microphone();

    // Start is called before the first frame update
    private void Awake()
    {

    }

    void Start()
    {
#if PLATFORM_ANDROID
        Permission.RequestUserPermission(Permission.Microphone);
        Permission.RequestUserPermission(Permission.CoarseLocation);
        Permission.RequestUserPermission(Permission.ExternalStorageRead);
        Permission.RequestUserPermission(Permission.ExternalStorageWrite);
        Permission.RequestUserPermission(Permission.FineLocation);
#endif

        m_BtnStart.onClick.AddListener(StartSpeechRecognition);
        m_BtnStop.onClick.AddListener(StopSpeechRecognition);

        ISpeech.onSpeechRecognitionFinished += OnSpeechRecognitionFinished;
        ISpeech.onSpeechRecognitionDebug += OnSpeechRecognitionDebug;
        /*
        AndroidJavaClass activityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        unityContext = activityClass.GetStatic<AndroidJavaObject>("currentActivity");

        AndroidJavaClass pluginClass = new AndroidJavaClass("com.example.unitywrapper.USpeech");
        pluginUSpeechInstance = pluginClass.CallStatic<AndroidJavaObject>("CreateInstance", unityContext, m_ISpeech);
        */

        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        unityContext = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        //pluginUSpeechInstance = new AndroidJavaObject("com.example.unitywrapper.USpeech", unityContext, new ISpeech());
        unityContext.Call("runOnUiThread", new AndroidJavaRunnable(() =>
        {
            pluginUSpeechInstance = new AndroidJavaObject("com.example.unitywrapper.USpeech", unityContext, new ISpeech());
        }));
    }

    void StartSpeechRecognition()
    {
        //unityContext.Call("runOnUiThread", new AndroidJavaRunnable(() => { pluginUSpeechInstance.Call("StartSpeechRecognition"); }));

        //pluginUSpeechInstance.Call("StartSpeechRecognition");
        unityContext.Call("runOnUiThread", new AndroidJavaRunnable(() =>
        {
            pluginUSpeechInstance.Call("StartSpeechRecognition");
        }));
    }

    void StopSpeechRecognition()
    {
        //unityContext.Call("runOnUiThread", new AndroidJavaRunnable(() => { pluginUSpeechInstance.Call("StopSpeechRecognition"); }));

        //pluginUSpeechInstance.Call("StopSpeechRecognition");

        unityContext.Call("runOnUiThread", new AndroidJavaRunnable(() =>
        {
            pluginUSpeechInstance.Call("StopSpeechRecognition");
        }));
    }
    
    void OnSpeechRecognitionFinished(object sender, string text)
    {
        m_Text.text = text;
    }

    void OnSpeechRecognitionDebug(object sender, string text)
    {
        m_DebugText.text = text;
    }
}
