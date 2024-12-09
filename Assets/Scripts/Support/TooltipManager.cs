using System.Collections.Generic;
using System.Linq;
using System.Threading;
using CommonGameObjectParts;
using Cysharp.Threading.Tasks;
using UnityEngine;

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
            
            var tooltip = entity.GetComponentInChildren<Tooltip>(true);
            
            if (tooltip == null)
            {
                Debug.LogWarning($"No tooltip found for {entity.name}");
                return;
            }

            _tooltips.Add(entity, tooltip);
            TriggerIfCentered(entity, this.GetCancellationTokenOnDestroy()).Forget();
        }

        public void ForgetTooltip(InstantiatableEntity entity)
        {
            if (!_tooltips.TryGetValue(entity, out var tooltip))
                return;

            tooltip.HideTooltip();
            _tooltips.Remove(entity);
        }

        async UniTask TriggerIfCentered(InstantiatableEntity entity, CancellationToken token)
        {
            if (!_tooltips.TryGetValue(entity, out var tooltip))
                return;
            
            while (!token.IsCancellationRequested 
                   && entity != null 
                   && _tooltips.ContainsKey(entity))
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

        static bool IsObjectCenteredOnCamera(Tooltip tooltip)
        {
            if (Camera.allCameras.Length == 0)
                return false;
            
            var camera = Camera.allCameras[0];
            var screenPoint = camera.WorldToViewportPoint(tooltip.transform.position);
            return screenPoint is { z: > 0, x: > 0.45f and < 0.55f, y: > 0.45f and < 0.55f };
        }
    }
}