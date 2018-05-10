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
        private Transform _Player;
        [SerializeField]
        private float _SmoothSpeed;
        [SerializeField]
        private float _XThreshold;
        
        private Vector3 _TargetPosition;
        private MoveDirection _CurrentDir;
        private MoveDirection _NewDir;
        private float _PlayerDistance;

        private void Start()
        {
            _SmoothSpeed = 3.0f;
            _XThreshold = 2.0f;
        }

        private void LateUpdate()
        {
            MoveToPlayer();
        }

        private void MoveToPlayer()
        {
            _PlayerDistance = _Player.position.x - transform.position.x;
            SetNewDirection();

            if (_NewDir == MoveDirection.Still)
            {
                return;
            }

            _TargetPosition = new Vector3(_Player.position.x, transform.position.y, transform.position.z);

            if (_NewDir != _CurrentDir && Mathf.Abs(_PlayerDistance) > _XThreshold)
            {
                _CurrentDir = _NewDir;
            }

            SetPosition();
        }

        private void SetNewDirection()
        {
            if (_PlayerDistance > 0)
            {
                _NewDir = MoveDirection.Right;
            }
            else if (_PlayerDistance < 0)
            {
                _NewDir = MoveDirection.Left;
            }
            else
            {
                _NewDir = MoveDirection.Still;
            }
        }

        private void SetPosition()
        {
            transform.position = Vector3.Lerp(transform.position, _TargetPosition, _SmoothSpeed * Time.deltaTime);
        }
    }
}
