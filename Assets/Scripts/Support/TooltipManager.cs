using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using CommonGameObjectParts;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Support
{
    public class TooltipManager : MonoBehaviour
    {
        readonly Dictionary<InstantiatableEntity, Tooltip> _tooltips = new();

        void OnDestroy()
        {
            foreach (var tooltip in _tooltips.Values.Where(tooltip => tooltip != null))
            {
                Destroy(tooltip.gameObject);
            }
        }
        
        public void RememberTooltip(InstantiatableEntity entity)
        {
            if (_tooltips.ContainsKey(entity))
                return;
            
            var tooltip = entity.GetComponentInChildren<Tooltip>();
            _tooltips.Add(entity, tooltip);
            
        }

        public void ForgetTooltip(InstantiatableEntity entity)
        {
            _tooltips.Remove(entity);
        }

        async UniTask TriggerIfCentered(Tooltip tooltip, CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                if (IsObjectCenteredOnCamera(tooltip))
                {
                    tooltip.ShowTooltip();
                }
                else
                {
                    tooltip.HideTooltip();
                }
                await UniTask.NextFrame();
            }
        }
        
        bool IsObjectCenteredOnCamera(Tooltip tooltip)
        {
            if (Camera.main == null)
                return false;

            var screenPoint = Camera.main.WorldToViewportPoint(tooltip.transform.position);
            return screenPoint is { z: > 0, x: > 0 and < 1, y: > 0 and < 1 };

        }
    }
}