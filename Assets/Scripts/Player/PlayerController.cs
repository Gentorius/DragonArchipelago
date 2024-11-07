using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerController : IPlayerController
    {
        readonly PlayerInfo _playerInfo;
        readonly IPlayerSpawner _playerSpawner;
        readonly IPlayerCharacter _playerCharacter;
        CancellationTokenSource _cancellationTokenSource;
        
        InputAction _moveAction;
        InputAction _jumpAction;
        InputAction _attackAction;
        InputAction _interactAction;
        InputAction _lookAction;
        
        public PlayerController(IPlayerSpawner playerSpawner)
        {
            _playerSpawner = playerSpawner;
            _playerInfo = new PlayerInfo();
            _cancellationTokenSource = new CancellationTokenSource();
            var cancellationToken = _cancellationTokenSource.Token;
            _playerCharacter = _playerSpawner.SpawnPlayer(_playerInfo, cancellationToken);
            _playerCharacter.OnDestroyed += Dispose;
            
            _moveAction = InputSystem.actions.FindAction("Move");
            _jumpAction = InputSystem.actions.FindAction("Jump");
            _attackAction = InputSystem.actions.FindAction("Attack");
            _interactAction = InputSystem.actions.FindAction("Interact");
            _lookAction = InputSystem.actions.FindAction("Look");
            
            _moveAction.started += _ => Move();
            _jumpAction.performed += _ => Jump();
            _attackAction.performed += _ => Attack();
            _interactAction.performed += _ => Interact();
            _lookAction.started += _ => Look();
        }
        
        async UniTask Move()
        {
            while (_moveAction.IsPressed())
            {
                var moveValue = _moveAction.ReadValue<Vector2>();
                _playerCharacter.Move(moveValue);
                await UniTask.NextFrame();
            }
        }
        
        void Jump()
        {
            _playerCharacter.Jump();
        }
        
        void Attack()
        {
            _playerCharacter.Attack();
        }
        
        void Interact()
        {
            _playerCharacter.Interact();
        }
        
        void Look()
        {
            var lookValue = _lookAction.ReadValue<Vector2>();
            _playerCharacter.Look(lookValue);
        }

        void Dispose()
        {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
            
            if (_playerCharacter != null)
                _playerCharacter.OnDestroyed -= Dispose;
        }
    }
    
    public interface IPlayerController
    {
        
    }
}