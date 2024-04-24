using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [Header("Component")]

    public TextMeshProUGUI timerText;

    [Header("Timer Settings")]
    public float currentTime;
    public bool countUp;


    [Header("Limit Settings")]
    public bool hasLimit;
    public float timerLimit;


    [Header("Format Settings")]
    public bool hasFormat;
    public TimerFormats format;
    private Dictionary<TimerFormats, string> timeFormats = new Dictionary<TimerFormats, string>();
    // Start is called before the first frame update
    void Start()
    {
        timeFormats.Add(TimerFormats.Whole, "0");
        timeFormats.Add(TimerFormats.TenthDecimal, "0.0");
        timeFormats.Add(TimerFormats.HundrethsDecimal, "0.00");
        timeFormats.Add(TimerFormats.ThousathsDecimal, "0.000");
        countUp = true;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime = countUp ? currentTime += Time.deltaTime : currentTime -= Time.deltaTime;
        
        if (hasLimit && ((countUp && currentTime <= timerLimit) || (!countUp && currentTime > timerLimit)))
        {
            // currentTime = timerLimit;

            // Changing to red (melewati limit jika ada)
            SetTimerText();
            timerText.color = Color.red;

            // enabled = false;
        }

        SetTimerText();



    }
    private void SetTimerText()
    {
        timerText.text = hasFormat ? currentTime.ToString(timeFormats[format]) : currentTime.ToString();
    }
}

public enum TimerFormats
{
    Whole,
    TenthDecimal,
    HundrethsDecimal,
    ThousathsDecimal,
}
