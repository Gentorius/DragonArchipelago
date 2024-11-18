using System;

namespace CommonGameObjectInterfaces
{
    public interface IInteractable
    {
        public event Action OnInteracted;
        
        public IInteractable Interact();
    }
}