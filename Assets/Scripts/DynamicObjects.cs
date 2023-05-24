using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicObjects : MonoBehaviour
{

    public int numObjects = 100;
    public GameObject prefab;

    public float cellWidth = 200.0f;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < numObjects; i++)
        {
            Instantiate(prefab, transform).SetActive(true);
        }

        float contentWidth = numObjects * cellWidth;
        var rectTransform = GetComponent<RectTransform>();
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, contentWidth);
    }

}
