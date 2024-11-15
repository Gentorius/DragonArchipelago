using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerController : IPlayerController
    {
        readonly IPlayerCharacter _playerCharacter;
        readonly CancellationTokenSource _cancellationTokenSource;
        CancellationToken CancellationToken => _cancellationTokenSource.Token;

        readonly InputAction _moveAction;
        readonly InputAction _jumpAction;
        readonly InputAction _attackAction;
        readonly InputAction _interactAction;
        readonly InputAction _lookAction;
        
        bool _isJumping;
        
        public PlayerController(IPlayerSpawner playerSpawner)
        {
            var playerInfo = new PlayerInfo();
            _cancellationTokenSource = new CancellationTokenSource();
            _playerCharacter = playerSpawner.SpawnPlayer(playerInfo, CancellationToken);
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
                
                if(!_isJumping)
                    _playerCharacter.Move(moveValue);
                
                await UniTask.NextFrame();
            }
            _playerCharacter.StopMoving();
        }
        
        async UniTask Jump()
        {
            _isJumping = true;
            await _playerCharacter.Jump(CancellationToken);
            _isJumping = false;
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