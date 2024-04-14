using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Позаимствованный скрипт вращения камеры
public class cameraRotation : MonoBehaviour
{
    public Camera sceneCamera;
    public Transform target;

    [Range(5f, 15f)]

    public float mouseRotateSpeed = 5f;


    public float slerpSmoothValue = 0.3f;
    public float scrollSmoothTime = 0.12f;
    public float editorFOVSensitivity = 5f;

    private bool canRotate = true;

    private Vector2 swipeDirection; //swipe delta vector2
    private Quaternion currentRot; // store the quaternion after the slerp operation 
    private Quaternion targetRot;

    //Mouse rotation related
    private float rotX; // around x
    private float rotY; // around y
    //Mouse Scroll
    private float cameraFieldOfView;
    private float cameraFOVDamp; //Damped value
    private float fovChangeVelocity = 0;

    private float distanceBetweenCameraAndTarget;
    //Clamp Value

    [SerializeField] private float minXRotAngle = -65; 
    [SerializeField] private float maxXRotAngle = 60; 

    [SerializeField] private float minCameraFieldOfView = 10; 
    [SerializeField] private float maxCameraFieldOfView = 50; 

    Vector3 dir;
    private void Awake()
    {
        GetCameraReference();

    }
    // Start is called before the first frame update
    void Start()
    {
        distanceBetweenCameraAndTarget = Vector3.Distance(sceneCamera.transform.position, target.position);
        //Äîáàâèòü äðóãîé âàðèàíò???
        dir = new Vector3(3, 3, distanceBetweenCameraAndTarget);
        sceneCamera.transform.position = target.position + dir;

        cameraFOVDamp = sceneCamera.fieldOfView;
        cameraFieldOfView = sceneCamera.fieldOfView;
    }

    // Update is called once per frame
    void Update()
    {
        if (!canRotate)
        {
            return;
        }
        //We are in editor
        if (Application.isEditor || Application.platform == RuntimePlatform.WindowsPlayer)
        {
            EditorCameraInput();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            FrontView();
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            TopView();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            LeftView();
        }

    }

    private void LateUpdate()
    {
        RotateCamera();
        SetCameraFOV();
    }

    public void GetCameraReference()
    {
        if (sceneCamera == null)
        {
            sceneCamera = Camera.main;
        }

    }

    //May be the problem with Euler angles
    public void TopView()
    {
        rotX = -85;
        rotY = 0;
    }

    public void LeftView()
    {
        rotY = 90;
        rotX = 0;
    }

    public void FrontView()
    {
        rotX = 0;
        rotY = 0;
    }

    private void EditorCameraInput()
    {
        //Camera Rotation
        if (Input.GetMouseButton(1))
        {
            rotX += Input.GetAxis("Mouse Y") * mouseRotateSpeed; // around X
            rotY += Input.GetAxis("Mouse X") * mouseRotateSpeed;

            if (rotX < minXRotAngle)
            {
                rotX = minXRotAngle;
            }
            else if (rotX > maxXRotAngle)
            {
                rotX = maxXRotAngle;
            }
        }
        //Camera Field Of View
        if (Input.mouseScrollDelta.magnitude > 0)
        {
            cameraFieldOfView += Input.mouseScrollDelta.y * editorFOVSensitivity * -1;
        }
    }


    private void RotateCamera()
    {

        if (Application.isEditor || Application.platform == RuntimePlatform.WindowsPlayer)
        {
            Vector3 tempV = new Vector3(rotX, rotY, 0);
            targetRot = Quaternion.Euler(tempV); 
        }
        else
        {
            targetRot = Quaternion.Euler(-swipeDirection.y, swipeDirection.x, 0);
        }
        //Rotate Camera
        currentRot = Quaternion.Slerp(currentRot, targetRot, Time.smoothDeltaTime * slerpSmoothValue * 50);  
        sceneCamera.transform.position = target.position + currentRot * dir;
        sceneCamera.transform.LookAt(target.position);

    }

    void SetCameraFOV()
    {
        //Set Camera Field Of View
        //Clamp Camera FOV value
        if (cameraFieldOfView <= minCameraFieldOfView)
        {
            cameraFieldOfView = minCameraFieldOfView;
        }
        else if (cameraFieldOfView >= maxCameraFieldOfView)
        {
            cameraFieldOfView = maxCameraFieldOfView;
        }

        cameraFOVDamp = Mathf.SmoothDamp(cameraFOVDamp, cameraFieldOfView, ref fovChangeVelocity, scrollSmoothTime);
        sceneCamera.fieldOfView = cameraFOVDamp;

    }
}
