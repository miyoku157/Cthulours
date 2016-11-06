using UnityEngine;
using System.Collections;
namespace AssemblyCSharp
{
    public class ReturnMenu : MonoBehaviour
    {
        private void Awake()
        {
            AudioSource[] s = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
            foreach (AudioSource audio in s)
            {
                audio.mute = Changement.mute;
                audio.volume = Changement.volume;
            }
        }
        private void Update()
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                Application.LoadLevel("menu");
            }
        }
    }
}