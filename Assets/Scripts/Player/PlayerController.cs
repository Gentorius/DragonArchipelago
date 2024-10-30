using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerController : IPlayerController
    {
        readonly PlayerInfo _playerInfo;
        readonly IPlayerSpawner _playerSpawner;
        readonly IPlayerCharacter _playerCharacter;
        
        InputAction _moveAction;
        InputAction _jumpAction;
        InputAction _attackAction;
        InputAction _interactAction;
        InputAction _lookAction;
        
        public PlayerController(IPlayerSpawner playerSpawner)
        {
            _playerSpawner = playerSpawner;
            _playerInfo = new PlayerInfo();
            _playerCharacter = _playerSpawner.SpawnPlayer(_playerInfo);
            
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
        
        void Move()
        {
            var moveValue = _moveAction.ReadValue<Vector2>();
            _playerCharacter.Move(moveValue);
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
    }
    
    public interface IPlayerController
    {
        
    }
}