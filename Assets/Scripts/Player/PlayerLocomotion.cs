using UnityEngine;

namespace Com.DarkLynxDEV.Player
{
    public class PlayerLocomotion : MonoBehaviour
    {

        #region Variables

        [SerializeField] private Transform orientationTarget;

        [SerializeField] private float moveSpeed;
        private float playerMoveSpeed;

        [SerializeField] private float groundDrag = 5f;
        [SerializeField] private float airDrag = 0f;
        [SerializeField] private float airMultiplier = 0.4f;

        [SerializeField] private Transform groundCheck;
        [SerializeField] private LayerMask groundLayer;

        [SerializeField] private float jumpStrength;
        [SerializeField] private int jumpCount = 2;
        private int jumpsLeft;

        private Vector3 moveDirection;
        private Vector3 slopeMoveDirection;
        private Rigidbody rb;

        private RaycastHit slopeInfo;

        #endregion

        #region MonoBehaviour Callbacks

        private void Start() {
            rb = GetComponent<Rigidbody>();
            jumpsLeft = jumpCount;
        }

        private void Update() {
            ManagePlayerDrag();
            ProcessSlopeMovement();
        }

        private void FixedUpdate() {
            MovePlayerRB();
        }

        #endregion

        #region Private Methods

        private void MovePlayerRB() {
            float currentMultiplier = isGrounded() ? 1f : airMultiplier;
            Vector3 desiredMoveDirection = isOnSlope() ? slopeMoveDirection : moveDirection;

            rb.AddForce(desiredMoveDirection.normalized * moveSpeed * 10f * currentMultiplier, ForceMode.Acceleration);
        }

        private void ManagePlayerDrag() {
            rb.drag = isGrounded() ? groundDrag : airDrag;
        }

        private void Jump() {
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            rb.AddForce(transform.up * jumpStrength, ForceMode.Impulse);
        }

        private void ProcessSlopeMovement() {
            slopeMoveDirection = Vector3.ProjectOnPlane(moveDirection, slopeInfo.normal);
        }

        private bool isGrounded() {
            return Physics.CheckSphere(groundCheck.position, 0.2f, groundLayer);
        }

        private bool isOnSlope()
        {
            if(Physics.Raycast(transform.position, Vector3.down, out slopeInfo, 0.5f))
            {
                if(slopeInfo.normal != Vector3.up)
                {
                    return true;
                } else {
                    return false;
                }
            }

        return false;
        }

        #endregion

        #region Public Methods

        public void ProcessMovement(Vector2 input) {
            moveDirection = orientationTarget.forward * input.y + orientationTarget.right * input.x;
        }

        public void ProcessJump(bool input)
        {
            if(input)
            {
                if(isGrounded() || jumpsLeft > 0)
                {
                    Jump();
                    jumpsLeft--;
                }
            }
        }

        public void ResetJumpCounter()
        {
            jumpsLeft = jumpCount;
        }

        #endregion

    }
}
