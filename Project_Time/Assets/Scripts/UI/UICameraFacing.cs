using UnityEngine;

namespace ProjectTime.UI
{
    public class UICameraFacing : MonoBehaviour
    {
        private Camera cameraToFace;


        private void Start()
        {
            cameraToFace = Camera.main;
        }

        private void LateUpdate()
        {
            transform.forward = cameraToFace.transform.forward;
        }
    }
}