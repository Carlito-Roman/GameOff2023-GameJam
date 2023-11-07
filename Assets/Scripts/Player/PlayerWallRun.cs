using UnityEngine;

namespace Com.DarkLynxDEV.Player
{
    public class PlayerWallRun : MonoBehaviour
    {

        #region Variables

        [SerializeField] private Transform orientationTarget;

        [SerializeField] private float wallDistance = 0.5f;
        [SerializeField] private float minimumJumpHeight = 1.5f;
        [SerializeField] private LayerMask wallLayer;
        private bool wallIsToLeft, wallIsToRight;
        private RaycastHit leftWallInfo, rightWallInfo;
        private bool jumpFromWall;

        [SerializeField] private float wallRunDownwardForce;
        [SerializeField] private float wallRunJumpForce;
        [SerializeField] private float wallJumpMultiplier = 60f;

        [SerializeField] private Camera camTarget;
        private float baseFOV;
        [SerializeField] private float wallRunFOV;
        [SerializeField] private float wallRunFOVSmoothing;
        [SerializeField] private float cameraTiltStrength;
        [SerializeField] private float cameraTiltSmoothing;
        public float tilt { get; private set; }

        private Rigidbody rb;

        #endregion

        #region MonoBehaviour Callbacks

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            baseFOV = camTarget.fieldOfView;
        }

        private void Update() {
            ManageWallCheck();

            if(CanWallRun())
            {
                if(wallIsToLeft || wallIsToRight) {
                    StartWallRun();
                } else {
                    EndWallRun();
                }
            } else {
                EndWallRun();
            }
        }

        #endregion

        #region Private Methods

        private void ManageWallCheck() {
            wallIsToLeft = Physics.Raycast(transform.position, -orientationTarget.right, out leftWallInfo, wallDistance, wallLayer);
            wallIsToRight = Physics.Raycast(transform.position, orientationTarget.right, out rightWallInfo, wallDistance, wallLayer);
        }

        private void StartWallRun()
        {
            rb.useGravity = false;
            rb.AddForce(Vector3.down * wallRunDownwardForce, ForceMode.Force);

            #region - FOV Change -

            camTarget.fieldOfView = Mathf.Lerp(camTarget.fieldOfView, wallRunFOV, Time.deltaTime * wallRunFOVSmoothing);

            #endregion

            #region - Camera Tilt -

            if (wallIsToLeft) {
                tilt = Mathf.Lerp(tilt, -cameraTiltStrength, Time.deltaTime * cameraTiltSmoothing);
            } else if (wallIsToRight){
                tilt = Mathf.Lerp(tilt, cameraTiltStrength, Time.deltaTime * cameraTiltSmoothing);
            }

            #endregion

            if (jumpFromWall)
            {
                if (wallIsToLeft)
                {
                    Vector3 wallJumpDirection = transform.up + leftWallInfo.normal;
                    rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                    rb.AddForce(wallJumpDirection * wallRunJumpForce * wallJumpMultiplier, ForceMode.Force);

                }
                else if (wallIsToRight)
                {
                    Vector3 wallJumpDirection = transform.up + rightWallInfo.normal;
                    rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                    rb.AddForce(wallJumpDirection * wallRunJumpForce * wallJumpMultiplier, ForceMode.Force);
                }
            }
        }

        private void EndWallRun () {
            rb.useGravity = true;

            #region - FOV Change -

            camTarget.fieldOfView = Mathf.Lerp(camTarget.fieldOfView, baseFOV, Time.deltaTime * wallRunFOVSmoothing);

            #endregion

            #region - Camera Tilt -

            tilt = Mathf.Lerp(tilt, 0, Time.deltaTime * cameraTiltSmoothing);

            #endregion
        }



        private bool CanWallRun()
        {
            return !Physics.Raycast(transform.position, Vector3.down, minimumJumpHeight);
        }


        #endregion

        #region Public Methods

        public void ProcessWallJump(bool input) {
            jumpFromWall = input;
        }

        #endregion

    }
}
