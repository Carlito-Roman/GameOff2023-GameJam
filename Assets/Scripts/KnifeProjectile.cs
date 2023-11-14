using UnityEngine;

public class KnifeProjectile : MonoBehaviour
{

    #region Variables

    private Rigidbody rb;

    #endregion

    #region MonoBehaviour Callbacks

    private void Start() => rb = GetComponent<Rigidbody>();

    private void OnCollisionEnter(Collision collision)
    {
        ObjScaleManager objScale = collision.gameObject.GetComponent<ObjScaleManager>();

        if(objScale != null) {
            objScale.ProcessScaleChange();
            Destroy(this.gameObject);
        } else {
            rb.isKinematic = true;
            Destroy(this.gameObject, 6f);
        }


        
    }

    #endregion

    #region Private Methods



    #endregion

    #region Public Methods



    #endregion

}
