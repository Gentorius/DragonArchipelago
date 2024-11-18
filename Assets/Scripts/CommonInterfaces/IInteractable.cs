using System;

namespace CommonInterfaces
{
    public interface IInteractable
    {
        public event Action OnInteracted;
        
        public IInteractable Interact();
    }
}