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

        IInteractable _interactable;
        string _objectName;
        
        const string BaseTooltipText = "Interact";
        
        void Start()
        {
            SetUpTooltip();
            HideTooltip();
        }
        
        void SetUpTooltip()
        {
            _interactable = transform.parent.GetComponent<IInteractable>();
            _objectName = _interactable.Name;
            _textMeshPro.text = $"{BaseTooltipText} \n{_objectName}";
            _background.size = new Vector2(_textMeshPro.preferredWidth + 0.5f, _textMeshPro.preferredHeight + 0.5f);
        }
        
        public void ShowTooltip()
        {
            gameObject.SetActive(true);
            RotateTowardsCamera();
            Debug.Log("Showing tooltip");
        }
        
        public void HideTooltip()
        {
            gameObject.SetActive(false);
            Debug.Log("Hiding tooltip");
        }
        
        void RotateTowardsCamera()
        {
            if (Camera.main != null)
                transform.LookAt(Camera.main.transform);
        }
    }
}