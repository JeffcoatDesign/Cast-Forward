using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerStates
{
    public class IStandingState : IPlayerState
    {
        private PlayerController _playerController;
        public void Enter (PlayerController playerController)
        {
            _playerController = playerController;
        }

        public void Exit ()
        {

        }

        public void HandleInput()
        {
            if (_playerController == null)
                return;
            if (_playerController.MovementInput.magnitude != 0)
                _playerController.SetState(new IWalkingState());
        }
    }
}