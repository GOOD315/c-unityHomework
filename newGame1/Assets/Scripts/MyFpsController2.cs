using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFpsController2
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(CapsuleCollider))]
    public class MyFpsController2 : MonoBehaviour
    {
        [Header("Movement Settings")]
        [Tooltip("How fast the player moves while walking and strafing."), SerializeField]
        private float walkingSpeed = 5f;

        [Tooltip("Approximately the amount of time it will take for the player to reach maximum running or walking speed."), SerializeField]
        private float movementSmoothness = 0.125f;

        [Tooltip("The names of the axes and buttons for Unity's Input Manager."), SerializeField]
        private FpsInput input;

        // MOVEMENT
        private ArcadeVelocity _velocityX;
        private ArcadeVelocity _velocityZ;
        private bool _isGrounded;

        // OTHER
        private Rigidbody _rigidbody;
        private CapsuleCollider _collider;


        // Start is called before the first frame update
        void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
            _collider = GetComponent<CapsuleCollider>();
            _velocityX = new ArcadeVelocity();
            _velocityZ = new ArcadeVelocity();
            Cursor.lockState = CursorLockMode.Locked;
        }


        void FixedUpdate()
        {
            MoveCharacter();
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void MoveCharacter()
        {
            var direction = new Vector3(input.Move, 0f, input.Strafe).normalized;
            var worldDirection = transform.TransformDirection(direction);
            var velocity = worldDirection * walkingSpeed;
            //Checks for collisions so that the character does not stuck when jumping against walls.
            /* var intersectsWall = CheckCollisionsWithWalls(velocity);
             if (intersectsWall)
             {
                 _velocityX.Current = _velocityZ.Current = 0f;
                 return;
             }*/

            var smoothX = _velocityX.Update(velocity.x, movementSmoothness);
            var smoothZ = _velocityZ.Update(velocity.z, movementSmoothness);
            var rigidbodyVelocity = _rigidbody.velocity;
            var force = new Vector3(smoothX - rigidbodyVelocity.x, 0f, smoothZ - rigidbodyVelocity.z);
            _rigidbody.AddForce(force, ForceMode.VelocityChange);
        }


        private class ArcadeVelocity
        {
            private float _current;
            private float _currentVelocity;

            public float Current
            {
                set { _current = value; }
            }

            public float Update(float target, float smoothTime)
            {
                return _current = Mathf.SmoothDamp(_current, target, ref _currentVelocity, smoothTime);
            }

        }

        /// Input mappings
       // [Serializable] - почему то выдавало ошибку, когда я эту строчку оставлял
        private class FpsInput
        {
            [Tooltip("The name of the virtual axis mapped to rotate the camera around the y axis."),
             SerializeField]
            private string rotateX = "Mouse X";

            [Tooltip("The name of the virtual axis mapped to rotate the camera around the x axis."),
             SerializeField]
            private string rotateY = "Mouse Y";

            [Tooltip("The name of the virtual axis mapped to move the character back and forth."),
             SerializeField]
            private string move = "Horizontal";

            [Tooltip("The name of the virtual axis mapped to move the character left and right."),
             SerializeField]
            private string strafe = "Vertical";

            [Tooltip("The name of the virtual button mapped to run."),
             SerializeField]
            private string run = "Fire3";

            [Tooltip("The name of the virtual button mapped to jump."),
             SerializeField]
            private string jump = "Jump";

            /// Returns the value of the virtual axis mapped to rotate the camera around the y axis.
            public float RotateX
            {
                get { return Input.GetAxisRaw(rotateX); }
            }

            /// Returns the value of the virtual axis mapped to rotate the camera around the x axis.        
            public float RotateY
            {
                get { return Input.GetAxisRaw(rotateY); }
            }

            /// Returns the value of the virtual axis mapped to move the character back and forth.        
            public float Move
            {
                get { return Input.GetAxisRaw(move); }
            }

            /// Returns the value of the virtual axis mapped to move the character left and right.         
            public float Strafe
            {
                get { return Input.GetAxisRaw(strafe); }
            }

            /// Returns true while the virtual button mapped to run is held down.          
            public bool Run
            {
                get { return Input.GetButton(run); }
            }

            /// Returns true during the frame the user pressed down the virtual button mapped to jump.          
            public bool Jump
            {
                get { return Input.GetButtonDown(jump); }
            }
        }
    }
}