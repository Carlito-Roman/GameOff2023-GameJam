using UnityEngine;

namespace Com.DarkLynxDEV.Player
{
    public class PlayerCollisionDetection : MonoBehaviour
    {

        private void OnCollisionEnter(Collision collision)
        {
            GetComponent<PlayerLocomotion>().ResetJumpCounter();
        }

    }
}
