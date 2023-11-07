using UnityEngine;

namespace Com.DarkLynxDEV.Player
{
    public class PlayerCamera : MonoBehaviour
    {

        #region Variables

        [SerializeField] private Transform camTarget;
        [SerializeField] private Transform orientationTarget;

        [SerializeField] private float mouseSensitivity;

        [SerializeField] private float pitchMinMax;
        private float yRotation;
        private float xRotation;

        private PlayerWallRun wallRun;



        #endregion

        #region MonoBehaviour Callbacks

        private void Start()
        {
            SetCursor();
            wallRun = GetComponent<PlayerWallRun>();
        }

        private void Update()
        {
            ManageCameraLook();
        }

        #endregion

        #region Private Methods

        private void ManageCameraLook() {
            camTarget.transform.rotation = Quaternion.Euler(xRotation, yRotation, wallRun.tilt);
            orientationTarget.transform.rotation = Quaternion.Euler(0f, yRotation, 0f);
        }

        private void SetCursor()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        #endregion

        #region Public Methods

        public void ProcessLook(Vector2 input)
        {
            yRotation += input.x * mouseSensitivity * 0.01f;
            xRotation -= input.y * mouseSensitivity * 0.01f;

            xRotation = Mathf.Clamp(xRotation, -pitchMinMax, pitchMinMax);
        }

        #endregion

    }
}
