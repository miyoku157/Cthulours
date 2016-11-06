using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
public class test : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        print(FooPluginFunction());
    }

    // Update is called once per frame
    void Update()
    {

    }
    [DllImport("dll_test")]
    private static extern float FooPluginFunction();
    protected virtual void Awake()
    {

        // Calls the FooPluginFunction inside the plugin
        // And prints 5 to the console
        print(FooPluginFunction());
    }
}

