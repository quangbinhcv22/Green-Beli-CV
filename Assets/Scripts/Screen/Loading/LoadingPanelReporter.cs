using System.Collections;
using System.Collections.Generic;
using Pattern;
using UnityEngine;
using UnityEngine.Events;

namespace GScreen
{
    public class LoadingPanelReporter : Singleton<LoadingPanelReporter>
    {
        public UnityAction<float> OnLoading;
        public UnityAction OnLoaded;

        public LoadingConfig config;

        private readonly List<string> _tasks = new List<string>();
        private readonly List<string> _loadedTasks = new List<string>();

        public void ResetTasks()
        {
            _tasks.Clear();
            _loadedTasks.Clear();
        }

        public void AddTask(string taskName)
        {
            _tasks.Add(taskName);
            UpdateProgress();
        }

        public void RemoveTask(string taskName)
        {
            _loadedTasks.Add(taskName);
            UpdateProgress();
        }

        private void UpdateProgress()
        {
            const int minTotalTaskValue = 1;
            const int loadedValue = 1;

            var totalTasks = Mathf.Clamp(_tasks.Count, minTotalTaskValue, _tasks.Count);
            var loadingProgress = (float) _loadedTasks.Count / totalTasks;

            OnLoading?.Invoke(loadingProgress);

            if (loadingProgress is loadedValue)
            {
                StartCoroutine(InvokeOnLoadedAfterTween());
            }

            IEnumerator InvokeOnLoadedAfterTween()
            {
                yield return new WaitForSeconds(config.GetFinalDelay());
                OnLoaded?.Invoke();
            }
        }
    }
}