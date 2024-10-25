using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "ScriptableObjectInstaller", menuName = "Installers/ScriptableObjectInstaller")]
public class ScriptableObjectInstaller : ScriptableObjectInstaller<ScriptableObjectInstaller>
{
    [SerializeField]
    ScriptableObject[] _scriptableObjects;
    public override void InstallBindings()
    {
        foreach (var scriptableObject in _scriptableObjects)
        {
            Container.Bind(scriptableObject.GetType()).FromInstance(scriptableObject).AsSingle();
        }
    }
}