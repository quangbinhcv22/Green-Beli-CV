using System.Collections;
using Pattern;
using UnityEngine;
using UnityEngine.Events;

namespace UI.ServerResponseHandler
{
    public class DelayTimer : Singleton<DelayTimer>
    {
        public void StartMethod(UnityAction callback, float delay)
        {
            StartCoroutine(StartMethodCoroutine(callback, delay));
        }

        private static IEnumerator StartMethodCoroutine(UnityAction callback, float delay)
        {
            yield return new WaitForSeconds(delay);
            callback?.Invoke();
        }
    }
}