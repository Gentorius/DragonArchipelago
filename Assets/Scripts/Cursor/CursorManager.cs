using UnityEngine;

namespace Cursor
{
    public class CursorManager : MonoBehaviour
    {
        void OnApplicationFocus(bool hasFocus)
        {
            if (hasFocus)
            {
                UnityEngine.Cursor.lockState = CursorLockMode.Locked;
                UnityEngine.Cursor.visible = false;
            }
            else
            {
                UnityEngine.Cursor.lockState = CursorLockMode.None;
                UnityEngine.Cursor.visible = true;
            }
        }
    }
}