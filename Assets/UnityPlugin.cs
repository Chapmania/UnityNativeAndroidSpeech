using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnityPlugin : MonoBehaviour
{
    public Text m_Text;

    // Start is called before the first frame update
    void Start()
    {
        EchoCallback.onEchoReceivedCalled += SetText;
        AndroidJavaObject pluginClass = new AndroidJavaObject("com.example.unitywrapper.EchoMaker");
        pluginClass.Call("echoText", new EchoCallback(), "Hell Android Plugin");

        //var plugin = new AndroidJavaClass("com.example.unitywrapper.PluginClass");
        //m_Text.text = plugin.CallStatic<string>("GetTextFromPlugin", 5);
    }


    public void SetText(object sender, string text)
    {
        m_Text.text = text;
    }
}

