using UnityEngine;
using System.Collections;
using UnityEngine.UI;


namespace AssemblyCSharp
{
    public class Changement : MonoBehaviour
    {
        static bool init = false;
        public static float volume = 1;
        public static bool mute = false;

        void Start()
        {
            if (init)
            {
                AudioSource[] s = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
                foreach (AudioSource audio in s)
                {
                    audio.mute = mute;
                    audio.volume = audio.volume * volume;
                }
                GameObject.Find("Canvas").transform.GetChild(2).GetChild(2).gameObject.GetComponent<Toggle>().isOn = mute;
                GameObject.Find("Canvas").transform.GetChild(2).GetChild(3).gameObject.GetComponent<Slider>().value = volume;
                init = false;
            }
            else
            {
                AudioSource[] s = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
                foreach (AudioSource audio in s)
                {
                    audio.mute = mute;
                    audio.volume = audio.volume * volume;

                }
            }
        }

        void Update()
        {
			if (Application.loadedLevelName == "menu") {
								if (Input.GetKeyDown (KeyCode.Escape) && GameObject.Find ("Canvas").transform.GetChild (2).gameObject.activeSelf) {
										GameObject.Find ("Canvas").transform.GetChild (1).gameObject.SetActive (true);
										GameObject.Find ("Canvas").transform.GetChild (2).gameObject.SetActive (false);
								}
						}
        }
        public void change()
        {
			Time.timeScale = 1;

            Application.LoadLevel(1);

        }
        public void quit()
        {
            Application.Quit();
        }
        public void option()
        {
            GameObject.Find("Canvas").transform.GetChild(1).gameObject.SetActive(false);
            GameObject.Find("Canvas").transform.GetChild(2).gameObject.SetActive(true);
        }
        public void Exit()
        {
            GameObject.Find("Canvas").transform.GetChild(1).gameObject.SetActive(true);
            GameObject.Find("Canvas").transform.GetChild(2).gameObject.SetActive(false);
        }
        public void OnValueChanged(bool param)
        {
            AudioSource[] s = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
            foreach (AudioSource audio in s)
            {
                audio.mute = param;
            }
            mute = param;

        }
        public void ChangeValue(float f)
        {
            AudioSource[] s = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
            foreach (AudioSource audio in s)
            {
                audio.volume =f;
            }
            volume = f;
        }
        public void Resume()
        {
            GameObject.Find("Canvas").SetActive(false);
            Time.timeScale = 1;
            Chargement_jeu.pause = false;
        }
        public void change2()
        {
            init = true;
			Time.timeScale = 1;

            Application.LoadLevel(0);

        }
    }
}
