using System;
using CommonInterfaces;
using UnityEngine;

namespace SmallCraftObjects
{
    public class Stick : MonoBehaviour, IInteractable
    {
        public event Action OnInteracted;
        
        public IInteractable Interact()
        {
            OnInteracted?.Invoke();
            return this;
        }
    }
    
}