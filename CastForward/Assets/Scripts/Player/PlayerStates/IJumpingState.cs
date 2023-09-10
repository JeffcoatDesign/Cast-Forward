using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerStates {
    public class IJumpingState : IPlayerState
    {
        PlayerController _pc;
        float _jumpStartTime;
        float _JumpBuffer = 0.2f;
        public void Enter(PlayerController playerController)
        {
            _pc = playerController;
            _jumpStartTime = Time.time;
            _pc.rb.AddForce(Vector3.up * _pc.JumpPower);
        }

        public void Exit()
        {

        }

        public void HandleInput ()
        {
            Vector3 inputVector = new(_pc.MovementInput.x * _pc.PlayerSpeed * _pc.StrafeModifier * _pc.SprintSpeed,
                       0, _pc.MovementInput.y * _pc.PlayerSpeed * _pc.SprintSpeed);
            inputVector = _pc.CameraForward * inputVector;
            _pc.rb.AddForce(inputVector, ForceMode.VelocityChange);

            if (_pc.isGrounded && Time.time - _jumpStartTime > _JumpBuffer)
                _pc.SetState(new IWalkingState());
        }
    }
}