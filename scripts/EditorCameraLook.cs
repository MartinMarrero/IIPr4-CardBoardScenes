
using UnityEngine;

public class EditorCameraLook : MonoBehaviour
{
    public float sensitivity = 2f;

    private float rotationX;
    private float rotationY;

    private void Update()
    {
        if (!Input.GetMouseButton(1))
            return;

        rotationX += Input.GetAxis("Mouse X") * sensitivity;
        rotationY -= Input.GetAxis("Mouse Y") * sensitivity;

        rotationY = Mathf.Clamp(rotationY, -80f, 80f);

        transform.localRotation =
            Quaternion.Euler(rotationY, rotationX, 0f);
    }
}
