using System;

namespace CommonGameObjectParts
{
    public interface IInteractable
    {
        public string Name { get; }
        public event Action OnInteracted;
        public IInteractable Interact();
    }
}