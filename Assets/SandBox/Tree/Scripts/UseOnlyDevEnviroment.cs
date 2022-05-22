using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UseOnlyDevEnviroment : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    
#if GRBE_PRODUCTION

    private void Awake()
    {
        GetComponent<Button>().interactable = false;
        text.color = new Color(0.6f,0.6f,0.6f,1);
    }
#endif
}

