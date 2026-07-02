using UnityEngine;
using Unity.Cinemachine;
using StarterAssets;
using Unity.VisualScripting;
using UnityEngine.InputSystem;

public class ThirdPersonShooterController : MonoBehaviour
{
    [SerializeField] private CinemachineCamera aimVirtualCamera;
    [SerializeField] private float normalSensitivity;
    [SerializeField] private float aimSensitivity;
    [SerializeField] private LayerMask aimCollidermask;
    [SerializeField] private Transform debugTransform;
    [SerializeField] private Transform pfBulletProjectile;
    [SerializeField] private Transform spawnBulletPosition;

    private StarterAssetsInputs StarterAssetsInputs;
    private ThirdPersonController thirdpersoncontroller;

    private void Awake()
    {
        StarterAssetsInputs = GetComponent<StarterAssetsInputs>();
        thirdpersoncontroller = GetComponent<ThirdPersonController>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Detect hitpoint by raycast

        Vector3 mouseWorldPosition = Vector3.zero;
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);

        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        Transform hitTransform = null;

        if (Physics.Raycast(ray, out RaycastHit rayCastHit, 999f, aimCollidermask))
        {
            debugTransform.position = rayCastHit.point;
            mouseWorldPosition = rayCastHit.point;
            hitTransform = rayCastHit.transform;
        }

        // to active and deactive the aimvirtualcamera gameobject

        if (StarterAssetsInputs.aim)
        {
            aimVirtualCamera.gameObject.SetActive(true);
            thirdpersoncontroller.SetSensitivity(aimSensitivity);
            thirdpersoncontroller.SetRotateOnMove(false);

            Vector3 worldAimTarget = mouseWorldPosition;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;
            transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime*20f);
        }
        else {
            aimVirtualCamera .gameObject.SetActive(false);
            thirdpersoncontroller.SetSensitivity(normalSensitivity);
            thirdpersoncontroller.SetRotateOnMove(true);
        }

        if (StarterAssetsInputs.Shoot) {
            Vector3 aimDir = (mouseWorldPosition-spawnBulletPosition.position).normalized;
            Instantiate(pfBulletProjectile,spawnBulletPosition.position,Quaternion.LookRotation(aimDir, Vector3.up));
            StarterAssetsInputs.Shoot = false;
        }
    }
}
