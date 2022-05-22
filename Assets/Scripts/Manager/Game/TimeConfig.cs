using System;
using UnityEngine;

namespace Manager.Game
{
    [CreateAssetMenu(fileName = "TimeConfig", menuName = "ScriptableObjects/General/TimeConfig")]
    public class TimeConfig : ScriptableObject
    {
        public DateType useDateType;

        public enum DateType
        {
            Local = 0,
            Utc = 1,
            Server = 2,
        }
        
        public string FormattedTimeSpan(TimeSpan timeSpan)
        {
            return $"{timeSpan.Hours:D2}:{timeSpan.Minutes:D2}:{timeSpan.Seconds:D2}";
        }
    }
}