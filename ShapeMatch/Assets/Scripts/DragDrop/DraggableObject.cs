using UnityEngine;
using ShapeMatch.EventManagement;
using ShapeMatch.Gameplay;

namespace ShapeMatch.DragDrop
{
    public class DraggableObject : MonoBehaviour
    {
        private bool _isContained;
        private Shape _shape;
        private Vector3 _originalPosition;
        private bool _isDragging = false;
        private Container _currentContainer;

        private void Awake() 
        {
            _shape = GetComponent<Shape>();    
        }

        private void Start()
        {
            _originalPosition = transform.position;
        }

        void Update()
        {
            if (_isDragging && !_isContained)
            {
                // Convert the touch position to world space
                Vector3 touchPosition = Input.GetTouch(0).position;
                touchPosition.z = 10.0f; // Set a suitable z distance from the camera
                Vector3 worldPosition = HelperUtilities.GetMouseWorldPosition();

                // Update the object's position
                transform.position = new Vector3(worldPosition.x, worldPosition.y, _originalPosition.z);
            }

            // Handle touch ending (or mouse button release for testing in editor)
            if (Input.GetMouseButtonUp(0))
            {
                _isDragging = false;
            
                if (_currentContainer != null) 
                {
                    transform.position = _currentContainer.transform.position;
                    EventManager.Broadcast(GameEvent.OnShapeContained, _shape);
                    _isContained = true;
                }
                else 
                {
                    transform.position = _originalPosition;
                    EventManager.Broadcast(GameEvent.OnShapeContainFailed, _shape);   
                }
            }
        }

        private void OnMouseDown()
        {
            // Start dragging on touch or mouse down
            if (Input.GetMouseButtonDown(0))
            {
                _isDragging = true;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            // Check if the draggable object enters a valid container
            if (collision.gameObject.TryGetComponent(out Container container))
            {
                _currentContainer = container;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            // Clear the reference when exiting the container
            if (collision.gameObject.TryGetComponent(out Container container))
            {
                if (_currentContainer == container)
                {
                    _currentContainer = null;
                }
            }
        }
    }
}