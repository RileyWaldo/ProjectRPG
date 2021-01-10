using UnityEngine;
using Cinemachine;

namespace RPG.Core
{
    public class FollowCamera : MonoBehaviour
    {
        [Header("Camera Controls")]
        [SerializeField] KeyCode turnRight = KeyCode.D;
        [SerializeField] KeyCode turnLeft = KeyCode.A;
        [SerializeField] KeyCode zoomIn = KeyCode.W;
        [SerializeField] KeyCode zoomOut = KeyCode.S;
        [SerializeField] float turnSensitivity = 180f;
        [SerializeField] float zoomSensitivity = 8f;
        [SerializeField] float minZoom = 5;
        [SerializeField] float maxZoom = 15;

        CinemachineVirtualCamera followCam;
        CinemachineFramingTransposer framingTransposer;

        private void Start()
        {
            followCam = GetComponent<CinemachineVirtualCamera>();
            framingTransposer = followCam.GetCinemachineComponent<CinemachineFramingTransposer>();
        }

        private void LateUpdate()
        {
            ZoomCamera();
            RotateCamera();
        }

        private void ZoomCamera()
        {
            float currentCameraDist = framingTransposer.m_CameraDistance;
            if (Input.GetKey(zoomOut))
                currentCameraDist += zoomSensitivity * Time.deltaTime;
            if (Input.GetKey(zoomIn))
                currentCameraDist -= zoomSensitivity * Time.deltaTime;

            currentCameraDist = Mathf.Clamp(currentCameraDist, minZoom, maxZoom);

            framingTransposer.m_CameraDistance = currentCameraDist;
        }

        private void RotateCamera()
        {
            float turnAngle = 0f;

            if (Input.GetKey(turnRight))
                turnAngle -= turnSensitivity * Time.deltaTime;
            if (Input.GetKey(turnLeft))
                turnAngle += turnSensitivity * Time.deltaTime;

            transform.Rotate(new Vector3(0, turnAngle, 0), Space.World);
        }
    }
}