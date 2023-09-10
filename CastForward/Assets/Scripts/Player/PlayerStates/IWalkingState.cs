using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerStates {
    public class IWalkingState : IPlayerState
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
                _pc.SetState(new IStandingState());
            else if (_pc.jumpPressed)
                _pc.SetState(new IJumpingState());
            else
            {
                Vector3 inputVector = new (_pc.MovementInput.x * _pc.PlayerSpeed * _pc.StrafeModifier * _pc.SprintSpeed,
                    0, _pc.MovementInput.y * _pc.PlayerSpeed * _pc.SprintSpeed);
                inputVector = _pc.CameraForward * inputVector;
                _pc.rb.AddForce(inputVector, ForceMode.VelocityChange);
            }
        }
    }
}