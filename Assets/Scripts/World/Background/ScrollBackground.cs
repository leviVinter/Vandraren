using UnityEngine;

namespace Vandraren.World.Background
{
    public class ScrollBackground : MonoBehaviour
    {
        private Transform _camera;
        private Transform[] _layers;
        private int _leftIndex;
        private int _rightIndex;
        private float _lastCameraX;

        public float BackgroundWidth;
        public float ScrollOffset;
        public float ParalaxSpeed;

        private void Start()
        {
            _camera = Camera.main.transform;
            _lastCameraX = _camera.position.x;

            _layers = new Transform[transform.childCount];

            for (var i = 0; i < _layers.Length; i++)
            {
                _layers[i] = transform.GetChild(i);
            }

            _leftIndex = 0;
            _rightIndex = _layers.Length - 1;
        }

        private void Update()
        {
            var deltaX = _camera.position.x - _lastCameraX;
            transform.position += new Vector3(deltaX * ParalaxSpeed, 0, 0);

            _lastCameraX = _camera.position.x;

            if (_camera.position.x < (_layers[_leftIndex].transform.position.x + ScrollOffset))
            {
                ScrollLeft();
            }
            else if (_camera.position.x > (_layers[_rightIndex].transform.position.x - ScrollOffset))
            {
                ScrollRight();
            }
        }

        /// <summary>
        /// Switch the right most image to the left most position.
        /// </summary>
        private void ScrollLeft()
        {
            var newXPosition = _layers[_leftIndex].position.x - BackgroundWidth;
            _layers[_rightIndex].position = new Vector3(newXPosition, 0, 0);
            _leftIndex = _rightIndex--;

            if (_rightIndex < 0)
            {
                _rightIndex = _layers.Length - 1;
            }
        }

        /// <summary>
        /// Switch the left most image to the right most position.
        /// </summary>
        private void ScrollRight()
        {
            var newXPosition = _layers[_rightIndex].position.x + BackgroundWidth;
            _layers[_leftIndex].position = new Vector3(newXPosition, 0, 0);
            _rightIndex = _leftIndex++;

            if (_leftIndex >= _layers.Length)
            {
                _leftIndex = 0;
            }
        }
    }
}
