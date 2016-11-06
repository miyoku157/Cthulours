using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour
{
    private GameObject obj;
    private GUIText text;
    private static int seconds, minutes, hours = 0;

    public static int time = 0;

    // Use this for initialization
    void Awake()
    {
        seconds = time;
        obj = GameObject.Find("GUI/GUI Text");
        text = obj.GetComponent(typeof(GUIText)) as GUIText;
        StartCoroutine(Tick());
    }

    IEnumerator Tick()
    {
        for (; ; )
        {
            yield return new WaitForSeconds(1);
            Tack();
            if (minutes < 10 && seconds < 10)
            {
                text.text = "Time : 0" + minutes.ToString() + ":0" + seconds.ToString();
            }
            else if (minutes < 10)
            {
                text.text = "Time : 0" + minutes.ToString() + ":" + seconds.ToString();
            }
            else if (seconds < 10)
            {
                text.text = "Time : " + minutes.ToString() + ":0" + seconds.ToString();
            }
        }
    }

    void Tack()
    {
        time++;
        seconds++;

        if (seconds >= 60)
        {
            seconds -= 60;
            minutes++;
            if (minutes >= 60)
            {
                minutes -= 60;
                hours++;
            }
        }
    }

    public static int getTime()
    {
        return time;
    }

	public static void Initialize()
	{
		time = 0;
		seconds = 0;
		minutes = 0;
		hours = 0;
	}
}

