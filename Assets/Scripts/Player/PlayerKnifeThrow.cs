using System.Collections.Generic;
using UnityEngine;

namespace Com.DarkLynxDEV.Player
{
    public class PlayerKnifeThrow : MonoBehaviour
    {

        #region Variables

        [Header("Knife Throw - Cam Reference")]
        [SerializeField] private Camera playerFPSCamera;

        [Header("Knife Throw - Prefab Reference")]
        [SerializeField] private GameObject knifePrefab;

        [Header("Knife Throw - Firing Point/Barrel")]
        [SerializeField] private Transform firingPoint;
        [SerializeField] private float throwStrength;

        private Vector3 destination;

        #endregion

        #region Private Methods

        private void ThrowKnife()
        {
            //Find center of the screen
            Ray ray = playerFPSCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hitInfo;

            //Determin destination based on what the raycast hits
            destination = Physics.Raycast(ray, out hitInfo) ? hitInfo.point : ray.GetPoint(1000);

            //Throw knife at target
            ManageInstantiatedKnife();
        }

        private void ManageInstantiatedKnife()
        {
            var knifeObj = Instantiate(knifePrefab, firingPoint.position, playerFPSCamera.transform.rotation) as GameObject;
            knifeObj.GetComponent<Rigidbody>().velocity = (destination - firingPoint.position).normalized * throwStrength;
        }

        #endregion

        #region Public Methods

        public void ProcessKnifeThrow(bool input)
        {
            if (input) {
                ThrowKnife();
            }
        }

        #endregion

    }
}
