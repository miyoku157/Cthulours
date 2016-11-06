using UnityEngine;
using System.Collections;

public class Link : MonoBehaviour
{
    public string scene;

    private void OnMouseDown()
    {
        Application.LoadLevel(scene);
    }
}
