using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vandraren.World.Characters;

namespace Vandraren.View
{
    [RequireComponent(typeof(Camera))]
    public class CameraController : MonoBehaviour
    {
        private enum MoveDirection
        {
            Still,
            Left,
            Right
        }

        [SerializeField]
        private Transform _Follow;
        private Camera _MainCamera;
        public Vector3 _SpecificVector;
        [SerializeField]
        private float _SmoothSpeed;
        [SerializeField]
        private float _XThreshold;
        private MoveDirection _CurrentDir;

        private void Start()
        {
            _MainCamera = Camera.main;
            _SmoothSpeed = 3.0f;
            _XThreshold = 1.0f;
        }

        private void Update()
        {
            float xDistance = _Follow.position.x - transform.position.x;
            MoveDirection dir = CheckNewDirection(xDistance);
            if (dir == MoveDirection.Still)
            {
                return;
            }

            _SpecificVector = new Vector3(_Follow.position.x, transform.position.y, transform.position.z);

            if (dir != _CurrentDir)
            {
                if (Mathf.Abs(xDistance) > _XThreshold)
                {
                    _CurrentDir = dir;
                    transform.position = Vector3.Lerp(transform.position, _SpecificVector, _SmoothSpeed * Time.deltaTime);
                }
            }
            else if (dir == _CurrentDir)
            {
                transform.position = Vector3.Lerp(transform.position, _SpecificVector, _SmoothSpeed * Time.deltaTime);
            }
        }

        private MoveDirection CheckNewDirection(float pDistance)
        {
            MoveDirection dir = MoveDirection.Still;
            if (pDistance > 0)
            {
                dir = MoveDirection.Right;
            }
            else if (pDistance < 0)
            {
                dir = MoveDirection.Left;
            }

            return dir;
        }
    }
}
