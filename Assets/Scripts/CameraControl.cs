//  Skriptet lånat från
//  Gist på Github: https://gist.github.com/3dln/c16d000b174f7ccf6df9a1cb0cef7f80
//  med en del modifieringar för att passa.

using System;
using UnityEngine;
using static PauseFunction;

    public class CameraControl : MonoBehaviour
    {
        public GameObject target;
        public float distance = 40.0f;
        public float xSpeed = 250.0f;
        public float ySpeed = 120.0f;
        public float yMinLimit = 50;
        public float yMaxLimit = 80;
        public float zoomSpeed = 20.0f;
        public float zoomMax = 600.0f;
        public float zoomMin = 20.0f;

        float x = 0.0f;
        float y = 0.0f;

        void Start()
        {
            var angles = transform.eulerAngles;
            x = angles.y;
            y = angles.x;
        }

        float prevDistance;

        void LateUpdate()
        {
            if(!isPaused)
            {
                if(CanZoom(distance, zoomMin, zoomMax, (Input.GetAxis("Mouse ScrollWheel"))))
                    distance -= (Input.GetAxis("Mouse ScrollWheel") * zoomSpeed);


                if (target)
                {
                    var pos = Input.mousePosition;
                    var dpiScale = 1f;
                    if (Screen.dpi < 1) dpiScale = 1;
                    if (Screen.dpi < 200) dpiScale = 1;
                    else dpiScale = Screen.dpi / 200f;

                    if (pos.x < 380 * dpiScale && Screen.height - pos.y < 250 * dpiScale) return;

                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;


                    x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
                    y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

                    y = ClampAngle(y, yMinLimit, yMaxLimit);
                    var rotation = Quaternion.Euler(y, x, 0);
                    var position = rotation * new Vector3(0.0f, 0.0f, -distance) + target.transform.position;
                    transform.rotation = rotation;
                    transform.position = position;

                }
                else
                {
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                }

                if (Math.Abs(prevDistance - distance) > 0.001f)
                {
                    prevDistance = distance;
                    var rot = Quaternion.Euler(y, x, 0);
                    var po = rot * new Vector3(0.0f, 0.0f, -distance) + target.transform.position;
                    transform.rotation = rot;
                    transform.position = po;
                }
                

                /*static*/ float ClampAngle(float angle, float min, float max)
                {
                    if (angle < -360)
                        angle += 360;
                    if (angle > 360)
                        angle -= 360;
                    return Mathf.Clamp(angle, min, max);
                }
            }
        }

        static bool CanZoom(float distance, float min, float max, float scrollWheel)
        {
            bool canZoom;
            if(distance < min && scrollWheel > 0.0f || distance > max && scrollWheel < 0.0f)
                canZoom = false;
            else
                canZoom = true;
            return canZoom;
        }
    }