using UnityEngine;
using System.Collections;

public class GUIHandler : MonoBehaviour {
    public GameObject gen;
    GameObject scripter;
    boxScript script;
    public string textField;

    // Use this for initialization
    void Awake() {
        Debug.ClearDeveloperConsole();
        Debug.Log("GUIHandler initialized");
    }
    
    void OnGUI() {
        GUI.TextField(new Rect(0, 0, Screen.width, 50), textField);
        if(GUI.Button(new Rect(50,50,100,75),"Build Object")) {
            scripter = GameObject.Instantiate(gen);
            script = scripter.GetComponent<boxScript>();
        }
        if (GUI.Button(new Rect(175, 50, 100, 75), "Check Manual Inputs")) {
            if(script.useManual) {
                textField = "Check Manual Inputs(): " + script.checkInput();
            }
            else {
                textField = "boxScript useManual is not checked";
            }
        }
        if (GUI.Button(new Rect(295,50,100,75),"Make Room")) {
            if(script.Setup(scripter,this.gameObject) == 1) {
                script.build();
            }
        }
        if (GUI.Button(new Rect(415, 50, 100, 75), "Destroy")) {
            if (script.Setup(scripter, this.gameObject) == 1) {
                script.destroy();
            }
        }
    }
}
