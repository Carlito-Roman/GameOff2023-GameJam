using UnityEngine;

namespace Com.DarkLynxDEV.Player
{
    public class PlayerInputManager : MonoBehaviour
    {

        #region Variables

        [Header("Input - Movement")]
        private Vector2 moveInput;

        [Header("Input - Camera")]
        private Vector2 mouseInput;

        [Header("Input - Action Keys")]
        private bool jumpKey;

        [Header("Script References")]
        private PlayerLocomotion locomotion;
        private PlayerCamera camera;
        private PlayerWallRun wallRun;

        #endregion

        #region MonoBehaviour Callbacks

        private void Awake() => GetReferences();

        private void Update() {
            HandleAllInputs();

            locomotion.ProcessMovement(moveInput);
            camera.ProcessLook(mouseInput);

            locomotion.ProcessJump(jumpKey);
            wallRun.ProcessWallJump(jumpKey);
        }

        #endregion

        #region Private Methods

        private void HandleAllInputs()
        {
            HandleMovementInput();
            HandleCameraInput();
            HandleKeyInput();
        }

        #region - Player Input Variables/Declarations -

        private void HandleMovementInput()
        {
            moveInput.x = Input.GetAxisRaw("Horizontal");
            moveInput.y = Input.GetAxisRaw("Vertical");
        }

        private void HandleCameraInput()
        {
            mouseInput.x = Input.GetAxisRaw("Mouse X");
            mouseInput.y = Input.GetAxisRaw("Mouse Y");
        }

        private void HandleKeyInput()
        {
            jumpKey = Input.GetKeyDown(KeyCode.Space);
        }

        #endregion

        private void GetReferences()
        {
            locomotion = GetComponent<PlayerLocomotion>();
            camera = GetComponent<PlayerCamera>();
            wallRun = GetComponent<PlayerWallRun>();
        }

        #endregion

    }
}
