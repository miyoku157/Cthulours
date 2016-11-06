using UnityEngine;
using System.Collections;

public class Exit : MonoBehaviour
{
    private void OnMouseDown()
    {
        Application.Quit();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
