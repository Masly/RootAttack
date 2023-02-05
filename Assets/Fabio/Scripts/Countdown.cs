using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Countdown : MonoBehaviour
{
    public TextMeshProUGUI countDownText;
    private bool isCountdownAlreadyCalled = false;

    [Header("Countdown")]
    [SerializeField] private AudioClip timeOutClip;
    [SerializeField] private AudioClip countDownClip;

    public void StartCountdown()
    {
        if (!isCountdownAlreadyCalled)
        {
            isCountdownAlreadyCalled = true;
            StartCoroutine(CountDown());
        }

    }

    IEnumerator CountDown()
    {
        float effectTime = 1f;
        float time = 0f;
        float perc = 0f;
        float lastTime = Time.realtimeSinceStartup;

        AudioManager.i.PlayAudioSource(AudioManager.i.CountDown);

        countDownText.text = "3";
        countDownText.gameObject.SetActive(true);
        do
        {
            // we might use Time.deltaTime but won't work when Time.timeScale is 0
            time += Time.realtimeSinceStartup - lastTime;
            lastTime = Time.realtimeSinceStartup;
            perc = Mathf.Clamp01(time / effectTime);

            countDownText.GetComponent<CanvasGroup>().alpha = 1 - perc;

            yield return null;

        } while (perc < 1);

        countDownText.gameObject.SetActive(false);
        yield return null;

        time = 0;
        countDownText.text = "2";
        countDownText.gameObject.SetActive(true);
        AudioManager.i.PlayAudioSource(AudioManager.i.CountDown);

        do
        {
            // we might use Time.deltaTime but won't work when Time.timeScale is 0
            time += Time.realtimeSinceStartup - lastTime;
            lastTime = Time.realtimeSinceStartup;
            perc = Mathf.Clamp01(time / effectTime);

            countDownText.GetComponent<CanvasGroup>().alpha = 1 - perc;

            yield return null;

        } while (perc < 1);

        countDownText.gameObject.SetActive(false);
        yield return null;

        time = 0;
        countDownText.text = "1";
        countDownText.gameObject.SetActive(true);
        AudioManager.i.PlayAudioSource(AudioManager.i.CountDown);

        do
        {
            // we might use Time.deltaTime but won't work when Time.timeScale is 0
            time += Time.realtimeSinceStartup - lastTime;
            lastTime = Time.realtimeSinceStartup;
            perc = Mathf.Clamp01(time / effectTime);

            countDownText.GetComponent<CanvasGroup>().alpha = 1 - perc;

            yield return null;

        } while (perc < 1);

        countDownText.gameObject.SetActive(false);
        yield return null;


        time = 0;
        effectTime = 0.5f;
        countDownText.text = "GAME!";
        countDownText.gameObject.SetActive(true);
        do
        {
            // we might use Time.deltaTime but won't work when Time.timeScale is 0
            time += Time.realtimeSinceStartup - lastTime;
            lastTime = Time.realtimeSinceStartup;
            perc = Mathf.Clamp01(time / effectTime);

            countDownText.GetComponent<CanvasGroup>().alpha = 1 - perc;

            yield return null;

        } while (perc < 1);

        countDownText.gameObject.SetActive(false);


        countDownText.text = "";
        //isTimeToPlay = true;

        yield return null;
    }

    IEnumerator TimeUp()
    {
        float effectTime = 1f;
        float time = 0f;
        float perc = 0f;
        float lastTime = Time.realtimeSinceStartup;
        //isTimeUpDone = true;
        //endBell.Play();

        countDownText.text = "Prontoooo!";
        countDownText.gameObject.SetActive(true);
        do
        {
            // we might use Time.deltaTime but won't work when Time.timeScale is 0
            time += Time.realtimeSinceStartup - lastTime;
            lastTime = Time.realtimeSinceStartup;
            perc = Mathf.Clamp01(time / effectTime);

            countDownText.GetComponent<CanvasGroup>().alpha = 1 - perc;

            yield return null;

        } while (perc < 1);

        countDownText.gameObject.SetActive(false);


        // yield return null;
    }
}
