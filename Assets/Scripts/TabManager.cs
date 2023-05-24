using System.Collections;
using DanielLochner.Assets.SimpleScrollSnap;
using UnityEngine;

public class TabManager : MonoBehaviour
{
    public ScrollSnapRect SnapRect;
    public GameObject scrollBarObj;
    public ToggleAnim[] ToggleAnimList;

    [Header("Ease In")] public Vector3 targetPosition;
    public float duration = 1f;
    public AnimationCurve curve;
    private Vector3 initialPosition;

    void Start()
    {
        ToggleAnimList = GetComponentsInChildren<ToggleAnim>();
        SnapRect.onPanelSelected.AddListener(onPanelSelected);
        foreach (var toggle in ToggleAnimList)
        {
            toggle.Init();
        }

        onPanelSelected();
    }

    private void onPanelSelected()
    {
        var currentToggleIndex = SnapRect.TargetPanel;
        var currToggle = ToggleAnimList[currentToggleIndex];
        foreach (var toggle in ToggleAnimList)
        {
            toggle.OnToggleValueChanged(currToggle == toggle);
        }

        var destination = currToggle.transform.position;
        StartCoroutine(Move(scrollBarObj.transform, destination));
        Debug.Log("onPanelSelected: " + currentToggleIndex);
    }

    private IEnumerator Move(Transform objToMove, Vector3 destination)
    {
        initialPosition = objToMove.position;
        targetPosition = destination;
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float curveValue = curve.Evaluate(elapsedTime / duration);
            Vector3 newPosition = Vector3.Lerp(initialPosition, targetPosition, curveValue);
            objToMove.position = newPosition;
            yield return null;
        }

        objToMove.position = targetPosition;
    }
}
