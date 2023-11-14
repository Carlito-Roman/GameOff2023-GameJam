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

        //Find desired script
        ObjScaleManager objScale = collision.gameObject.GetComponent<ObjScaleManager>();


        //Check that it isn't null
        if(objScale != null) {

            //Call function
            objScale.ProcessScaleChange();

            //Destroy GameObject
            Destroy(this.gameObject);
        } else {

            //Otherwise, stop gameobject and destroy over time
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
