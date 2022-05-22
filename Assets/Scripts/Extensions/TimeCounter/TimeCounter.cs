using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Extensions.TimeCounter
{
    public class TimeCounter : MonoBehaviour
    {
        [SerializeField] private TMP_Text timeText;

        [SerializeField] private DisplayFormat displayFormat = DisplayFormat.MinutesSeconds;
        [SerializeField] private bool isShowSuffix = true;

        private bool _isResetCounting = false;

        private static TimeSpan DistanceBetween(DateTime fromDateTime, DateTime toDateTime)
        {
            return toDateTime - fromDateTime;
        }

        private static string GetFormattedTimeString(TimeSpan timeSpan, DisplayFormat format, bool isShowSuffix = true)
        {
            const string NONE_SUFFIX = "";

            var daySuffix = isShowSuffix ? " d" : NONE_SUFFIX;
            var hourSuffix = isShowSuffix ? " h" : NONE_SUFFIX;
            var minuteSuffix = isShowSuffix ? " m" : NONE_SUFFIX;
            var secondSuffix = isShowSuffix ? " s" : NONE_SUFFIX;

            switch (format)
            {
                case DisplayFormat.Seconds:
                    return $"{(int) timeSpan.TotalSeconds:D2}{secondSuffix}";
                case DisplayFormat.MinutesSeconds:
                    return
                        $"{(int) timeSpan.TotalMinutes:D2}{minuteSuffix} : {timeSpan.Seconds:D2}{secondSuffix}";
                case DisplayFormat.HoursMinutesSeconds:
                    return
                        $"{(int) timeSpan.TotalHours:D2}{hourSuffix} : {timeSpan.Minutes:D2}{minuteSuffix} : {timeSpan.Seconds:D2}{secondSuffix}";
                case DisplayFormat.DaysHoursMinutes:
                    return
                        $"{(int) timeSpan.TotalDays}{daySuffix} : {timeSpan.Hours:D2}{hourSuffix} : {timeSpan.Minutes:D2}{minuteSuffix}";
                case DisplayFormat.DaysHoursMinutesSeconds:
                    return
                        $"{(int) timeSpan.TotalDays}{daySuffix} : {timeSpan.Hours:D2}{hourSuffix} : {timeSpan.Minutes:D2}{minuteSuffix} : {timeSpan.Seconds:D2}{secondSuffix}";
            }

            return $"{timeSpan}";
        }

        private void OnDisable()
        {
            ResetCounting();
        }

        public IEnumerator CountingToDateTime(DateTime targetDateTime)
        {
            this._isResetCounting = false;

            timeText.text = GetFormattedTimeString(DistanceBetween(DateTime.Now, targetDateTime),
                displayFormat, isShowSuffix);

            //float numberSecondsToNextMinute = 60 - DateTime.Now.Second;
            var delayUpdateDisplay = new WaitForSeconds(1);

            while (DistanceBetween(DateTime.Now, targetDateTime).TotalSeconds >= 0)
            {
                if (this._isResetCounting)
                {
                    yield break;
                }

                timeText.text = GetFormattedTimeString(DistanceBetween(DateTime.Now, targetDateTime),
                    displayFormat, isShowSuffix);
                yield return delayUpdateDisplay;
            }
        }

        private void ResetCounting()
        {
            this._isResetCounting = true;
        }
    }
}