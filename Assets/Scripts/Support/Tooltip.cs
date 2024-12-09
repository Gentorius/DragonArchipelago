using CommonGameObjectParts;
using TMPro;
using UnityEngine;

namespace Support
{
    public class Tooltip : MonoBehaviour
    {
        [SerializeField]
        TextMeshPro _textMeshPro;
        [SerializeField]
        SpriteRenderer _background;
        [SerializeField]
        RectTransform _textRectTransform;

        IInteractable _interactable;
        string _objectName;
        
        Camera _camera;
        
        const string BaseTooltipText = "Interact";
        
        void Start()
        {
            if (Camera.allCamerasCount == 0)
                return;
            
            _camera = Camera.allCameras[0];
            SetUpTooltip();
            HideTooltip();
        }
        
        void SetUpTooltip()
        {
            _interactable = transform.parent.GetComponent<IInteractable>();
            _objectName = _interactable.Name;
            var text = $"{BaseTooltipText} \n{_objectName}";
            var size = _textMeshPro.GetPreferredValues(text);
            _textMeshPro.text = $"{BaseTooltipText} \n{_objectName}";
            _background.gameObject.transform.localScale = size;
            _textRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size.x);
            _textRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size.y);
        }
        
        public void ShowTooltip()
        {
            gameObject.SetActive(true);
            RotateTowardsCamera();
        }
        
        public void HideTooltip()
        {
            gameObject.SetActive(false);
        }
        
        void RotateTowardsCamera()
        {
            if (_camera == null)
            {
                if (Camera.allCamerasCount == 0)
                {
                    Debug.LogWarning("No cameras found");
                    return;
                }
                
                _camera = Camera.allCameras[0];
            }
            
            var cameraPosition = _camera.transform.position;
            transform.LookAt(cameraPosition);
            transform.right = -transform.right;
        }
    }
}