using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
     [SerializeField] private float moveSpeed = 3;
        [SerializeField] private float smoothMoveTime = 0.1f;
        [SerializeField] private float mouseSensitivity = 10;
        [SerializeField] private Vector2 pitchMinMax = new Vector2 (-40, 85);
        [SerializeField] private float rotationSmoothTime = 0.1f;

        private CharacterController _controller;
        private Camera _cam;
        private float _yaw;
        private float _pitch;
        private float _smoothYaw;
        private float _smoothPitch;
        private float _yawSmoothV;
        private float _pitchSmoothV;
        private Vector3 _velocity;
        private Vector3 _smoothV;
        private float _timer;
        
        private void Awake()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        
        private void Start () 
        {
            _cam = Camera.main;
            _controller = GetComponent<CharacterController> ();
            _yaw = transform.eulerAngles.y;
            if (_cam != null) _pitch = _cam.transform.localEulerAngles.x;
            _smoothYaw = _yaw;
            _smoothPitch = _pitch;
        }

        private void Update () 
        {
            
            Vector2 input = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
            
            Vector3 inputDir = new Vector3 (input.x, 0, input.y).normalized;
            Vector3 worldInputDir = transform.TransformDirection (inputDir);

            Vector3 targetVelocity = worldInputDir * moveSpeed;
            _velocity = Vector3.SmoothDamp (_velocity, targetVelocity, ref _smoothV, smoothMoveTime);
            _controller.Move (_velocity * Time.deltaTime);
            float mX = Input.GetAxisRaw ("Mouse X");
            float mY = Input.GetAxisRaw ("Mouse Y");

            float mMag = Mathf.Sqrt (mX * mX + mY * mY);
            if (mMag > 5) {
                mX = 0;
                mY = 0;
            }

            _yaw += mX * mouseSensitivity;
            _pitch -= mY * mouseSensitivity;
            _pitch = Mathf.Clamp (_pitch, pitchMinMax.x, pitchMinMax.y);
            _smoothPitch = Mathf.SmoothDampAngle (_smoothPitch, _pitch, ref _pitchSmoothV, rotationSmoothTime);
            _smoothYaw = Mathf.SmoothDampAngle (_smoothYaw, _yaw, ref _yawSmoothV, rotationSmoothTime);

            transform.eulerAngles = Vector3.up * _smoothYaw;
            _cam.transform.localEulerAngles = Vector3.right * _smoothPitch;

        }
}
