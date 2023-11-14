using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjScaleManager : MonoBehaviour
{

    #region Variables

    [Header("Object Scale - Animation Curve")]
    [SerializeField] private AnimationCurve curve;

    [Header("Object Scale - Target Scale")]
    [SerializeField] private Vector3 scaleGoal;
    private Vector3 originalScale;
    private Vector3 targetScaleGoal;

    [Header("Object Scale - Scale Lerp Variables")]
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
        //Change target based on bool
        targetState = !canScale ? 0 : 1;

        //Modify the speed at which I move from current to target
        currentState = Mathf.MoveTowards(currentState, targetState, scaleSmoothing * Time.deltaTime);

        //Modify Scale based on animation curve and current lerp value
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
