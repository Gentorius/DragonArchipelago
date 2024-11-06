using UnityEngine;

namespace Player
{
    public class CameraRig : MonoBehaviour
    {
        [SerializeField]
        float _rotationUpLimit = 30;
        [SerializeField]
        float _rotationDownLimit = 330;
        
        public void Look(Vector2 lookValue)
        {
            transform.Rotate(lookValue.y, lookValue.x, 0);
            var xRotation = transform.rotation.eulerAngles.x;
            
            if (xRotation > _rotationUpLimit && xRotation < 180)
            {
                xRotation = _rotationUpLimit; ;
            }
            
            if (xRotation < _rotationDownLimit && xRotation > 180)
            {
                xRotation = _rotationDownLimit;
            }
            
            transform.rotation = Quaternion.Euler(xRotation, transform.rotation.eulerAngles.y, 0);
        }
    }
}