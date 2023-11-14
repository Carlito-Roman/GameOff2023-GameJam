using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjScaleManager : MonoBehaviour
{

    #region Variables

    [SerializeField] private AnimationCurve curve;

    [SerializeField] private Vector3 scaleGoal;
    private Vector3 originalScale;
    private Vector3 targetScaleGoal;

    [SerializeField] private float scaleSmoothing;
    [SerializeField] private float scaleDuration;
    private float currentState, targetState;

    private bool canScale;

    #endregion

    #region MonoBehaviour Callbacks

    private void Start() => originalScale = transform.localScale;

    private void Update()
    {
        HandleObjectScale();
    }

    #endregion

    #region Private Methods

    private void HandleObjectScale()
    {
        targetState = !canScale ? 0 : 1;
        currentState = Mathf.MoveTowards(currentState, targetState, scaleSmoothing * Time.deltaTime);

        transform.localScale = Vector3.Lerp(originalScale, scaleGoal * curve.Evaluate(currentState), curve.Evaluate(currentState));
    }

    private IEnumerator SetScaleBool()
    {
        yield return new WaitForSeconds(0.1f);
        canScale = true;
        yield return new WaitForSeconds(scaleDuration);
        canScale = false;
    }


    #endregion

    #region Public Methods

    public void ProcessScaleChange() {
        StartCoroutine(SetScaleBool());
    }

    #endregion

}
