using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerStates
{
    public class PlayerStandingState : IPlayerState
    {
        private PlayerController _pc;
        public void Enter (PlayerController playerController)
        {
            _pc = playerController;
            _pc.ToggleSprint(false);
        }

        public void Exit ()
        {

        }

        public void HandleInput()
        {
            if (_pc == null)
                return;
            if (_pc.jumpPressed)
                _pc.SetState(new PlayerJumpingState());
            else if (_pc.IsCrouching)
                _pc.SetState(new PlayerCrouchState());
            else if (_pc.MovementInput.magnitude != 0)
                _pc.SetState(new PlayerWalkingState());
        }
    }
}