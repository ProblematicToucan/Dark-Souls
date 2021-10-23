using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GD
{
    public class GuiCameraFacing : MonoBehaviour
    {
        private void LateUpdate() {
            transform.LookAt(Camera.main.transform);
            transform.Rotate(0, 180, 0);
        }
    }
}