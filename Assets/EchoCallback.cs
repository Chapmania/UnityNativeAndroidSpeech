using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EchoCallback : AndroidJavaProxy
{
    public static event EventHandler<string> onEchoReceivedCalled = delegate { };

    public EchoCallback() : base("com.example.unitywrapper.EchoCallback") { }   
    
    public void onEchoReceived(string text)
    {
        Debug.Log(string.Format("Echo: {0}", text));
        onEchoReceivedCalled(this, text);
    }

}
