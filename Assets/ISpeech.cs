using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ISpeech : AndroidJavaProxy
{
    public static event EventHandler<string> onSpeechRecognitionFinished = delegate { };
    public static event EventHandler<string> onSpeechRecognitionDebug = delegate { };

    public ISpeech() : base("com.example.unitywrapper.ISpeech") { }

    public void onReceivedSpeech(string text)
    {
        onSpeechRecognitionFinished(this, text);
    }

    public void onDebugCallback(string text)
    {
        onSpeechRecognitionDebug(this, text);
    }
}
