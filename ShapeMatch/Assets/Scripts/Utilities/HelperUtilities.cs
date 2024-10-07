using UnityEngine;

namespace ShapeMatch
{
    public static class HelperUtilities
    {
        private static Camera _mainCam;

        /// <summary>
        /// Retrieves the world position of the mouse cursor.
        /// </summary>
        /// <returns>The world position of the mouse cursor as a Vector3.</returns>
        public static Vector3 GetMouseWorldPosition()
        {
            if (_mainCam == null) _mainCam = Camera.main;
            Vector3 mouseScreenPos = Input.mousePosition;

            mouseScreenPos.x = Mathf.Clamp(mouseScreenPos.x, 0f, Screen.width);
            mouseScreenPos.y = Mathf.Clamp(mouseScreenPos.y, 0f, Screen.height);

            Vector3 mouseWorldPos = _mainCam.ScreenToWorldPoint(mouseScreenPos);
            mouseWorldPos.z = 0;

            return mouseWorldPos;
        }
    }
}