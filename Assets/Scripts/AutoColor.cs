using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoColor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var images = GetComponentsInChildren<Image>();
        foreach (var img in images)
        {
            img.color = new Color(Random.value, Random.value, Random.value, 1.0f);
        }
    }
}
