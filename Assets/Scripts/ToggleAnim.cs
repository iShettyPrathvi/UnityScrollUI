using System.Collections;
using System.Collections.Generic;
using EasingAnimation;
using UnityEngine;
using UnityEngine.UI;

public class ToggleAnim : MonoBehaviour
{
    public LayoutElement myLayoutElement;

    public float DefaultWidth = 1.0f;
    public float SelectedWidth = 1.2f;
    public float scaleTo = 2.0f;
    public float resetDuration = 0.5f;

    [Header("Easing")]
    public float duration = 1f;
    public float amplitude = 1.0f;
    public float period = 0.3f;
    public bool useAnimCurve;
    public AnimationCurve animationCurve;
    
    private Coroutine resizeCoroutine;
    private GameObject childObj; //icon object
    
    public void Init()
    {
        myLayoutElement = GetComponent<LayoutElement>();

        //can be directly add reference in inspector
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
            : SmoothReset(myLayoutElement, DefaultWidth, 1.0f));
    }

    private IEnumerator SmoothResize(LayoutElement layoutElement, float newSize, float finalScaleY)
    {
        float currentWidth = layoutElement.flexibleWidth;
        Vector3 currentScale = childObj.transform.localScale;
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            //easing
            float t = elapsedTime / duration;
            float easedT = animationCurve.Evaluate(t);
                //Easing.EaseOutElastic(t, amplitude, period);

            float newWidth = Mathf.Lerp(currentWidth, newSize, easedT);
            myLayoutElement.flexibleWidth = newWidth;
            
            float newScale = Mathf.Lerp(currentScale.y, finalScaleY, easedT);
            childObj.transform.localScale = new Vector3(newScale, newScale, 1.0f);

            elapsedTime += Time.deltaTime;
            
            yield return null;
        }
        childObj.transform.localScale = new Vector3(finalScaleY, finalScaleY, 1.0f);
        myLayoutElement.flexibleWidth = newSize;
    }

    private IEnumerator SmoothReset(LayoutElement layoutElement, float newSize, float finalScaleY)
    {
        float currentWidth = layoutElement.flexibleWidth;
        Vector3 currentScale = childObj.transform.localScale;
        float elapsedTime = 0f;
        while (elapsedTime < resetDuration)
        {
            //easing
            float t = elapsedTime / resetDuration;
            float easedT = Easing.EaseOutExpo(t);

            float newWidth = Mathf.Lerp(currentWidth, newSize, easedT);
            myLayoutElement.flexibleWidth = newWidth;

            float newScale = Mathf.Lerp(currentScale.y, finalScaleY, easedT);
            childObj.transform.localScale = new Vector3(newScale, newScale, 1.0f);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        childObj.transform.localScale = new Vector3(finalScaleY, finalScaleY, 1.0f);
        myLayoutElement.flexibleWidth = newSize;
    }
}