using UnityEngine;
using UnityEngine.UI;
using System;

public class CalendarController : MonoBehaviour
{
    [SerializeField]
    private Text calendarText;
    [SerializeField]
    private Text clockText;

    void Start()
    {
        UpdateCalendar();
        UpdateClock();
    }

    void UpdateCalendar()
    {
        DateTime currentDate = DateTime.Now;
        int year = currentDate.Year;
        int month = currentDate.Month;

        DateTime firstDayOfMonth = new DateTime(year, month, 1);
        int daysInMonth = DateTime.DaysInMonth(year, month);

        string calendarString = $"{firstDayOfMonth:MMMM yyyy}\n";

        // æ“ª‚Ì—j“ú‚Ü‚Å‹ó”’‚ğ’Ç‰Á
        int daysFromPreviousMonth = (int)firstDayOfMonth.DayOfWeek;
        for (int i = 0; i < daysFromPreviousMonth; i++)
        {
            calendarString += "   ";
        }

        for (int day = 1; day <= daysInMonth; day++)
        {
            calendarString += $"{day,2} ";

            if ((daysFromPreviousMonth + day) % 7 == 0)
            {
                calendarString += "\n";
            }
        }

        calendarText.text = calendarString;
    }

    void UpdateClock()
    {
        DateTime currentTime = DateTime.Now;
        string clockString = currentTime.ToString("HH:mm:ss");
        clockText.text = clockString;
    }
}