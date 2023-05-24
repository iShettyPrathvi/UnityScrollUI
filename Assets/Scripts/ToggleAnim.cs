using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleAnim : MonoBehaviour
{
    public LayoutElement myLayoutElement;

    public float DefaultWidth = 1.0f;
    public float SelectedWidth = 1.2f;

    public float lerpSpeed = 1.0f;
    private Coroutine resizeCoroutine;

    public float scaleTo = 2.0f;
    private GameObject childObj;
    
    // Start is called before the first frame update
    public void Init()
    {
        myLayoutElement = GetComponent<LayoutElement>();

        childObj = this.transform.GetChild(0).gameObject;
    }

    public void OnToggleValueChanged(bool isOn)
    {
        if (resizeCoroutine != null)
        {
            StopCoroutine(resizeCoroutine);
        }

        Debug.Log("OnToggleValueChanged: " + isOn);
        resizeCoroutine = StartCoroutine(isOn
            ? SmoothResize(myLayoutElement, SelectedWidth, scaleTo)
            : SmoothResize(myLayoutElement, DefaultWidth, 1.0f));
    }

    private IEnumerator SmoothResize(LayoutElement layoutElement, float newSize, float finalScaleY)
    {
        float currentWidth = layoutElement.flexibleWidth;
        float currentScaleY = childObj.transform.localScale.y;
        float elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime * lerpSpeed;
            float newWidth = Mathf.Lerp(currentWidth, newSize, elapsedTime);
            myLayoutElement.flexibleWidth = newWidth;

            float newScaleY = Mathf.Lerp(currentScaleY, finalScaleY, elapsedTime);
            childObj.transform.localScale = new Vector3(newScaleY, newScaleY, 1.0f);

            yield return null;
        }
    }

}