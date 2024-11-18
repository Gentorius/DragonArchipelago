using System;
using CommonGameObjectInterfaces;
using UnityEngine;

namespace SmallCraftObjects
{
    public class Stick : MonoBehaviour, IInteractable, IPickable, ISpawnable
    {
        public event Action OnInteracted;
        
        public IInteractable Interact()
        {
            OnInteracted?.Invoke();
            return this;
        }
    }
    
}