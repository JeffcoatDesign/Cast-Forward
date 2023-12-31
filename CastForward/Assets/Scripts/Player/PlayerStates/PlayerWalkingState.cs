using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerStates {
    public class PlayerWalkingState : IPlayerState
    {
        private PlayerController _pc;

        public void Enter (PlayerController playerController)
        {
            _pc = playerController;
        }

        public void Exit ()
        {

        }

        public void HandleInput()
        {
            if (_pc == null)
                return;
            if (_pc.MovementInput.magnitude == 0)
                _pc.SetState(new PlayerStandingState());
            else if (_pc.IsCrouching)
            {
                if (_pc.SprintSpeed > 1f)
                    _pc.SetState(new PlayerSlideState());
                else
                _pc.SetState(new PlayerCrouchState());
            }
            else if (_pc.jumpPressed)
                _pc.SetState(new PlayerJumpingState());
            else
            {
                Vector3 inputVector = new(_pc.MovementInput.x * _pc.PlayerSpeed * _pc.StrafeModifier,
                    0, _pc.MovementInput.y * _pc.PlayerSpeed);
                if (inputVector.z < 0) inputVector.z *= _pc.ReverseModifier;
                inputVector *= _pc.SprintSpeed;
                inputVector = _pc.CameraForward * inputVector;
                _pc.rb.AddForce(inputVector, ForceMode.Force);
            }
        }
    }
}