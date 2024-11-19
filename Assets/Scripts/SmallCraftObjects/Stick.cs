using System;
using CommonGameObjectParts;

namespace SmallCraftObjects
{
    public class Stick : InstantiatableEntity, IInteractable, IPickable, ISpawnable
    {
        public event Action OnInteracted;
        public int ID => EntityID;
        public string Name => "Stick";
        
        public IInteractable Interact()
        {
            OnInteracted?.Invoke();
            return this;
        }
    }
    
}