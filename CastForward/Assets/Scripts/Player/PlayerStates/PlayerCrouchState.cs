using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerStates
{
    public class PlayerCrouchState : IPlayerState
    {
        private PlayerController _pc;
        private float _defYScale;

        public void Enter(PlayerController playerController)
        {
            _pc = playerController;
            _defYScale = _pc.transform.localScale.y;
            _pc.transform.localScale = new Vector3(_pc.transform.localScale.x, 0.5f, _pc.transform.localScale.z);
            _pc.rb.AddForce(Vector3.down * 5, ForceMode.Impulse);
        }

        public void Exit()
        {
            _pc.transform.localScale = new Vector3(_pc.transform.localScale.x, _defYScale, _pc.transform.localScale.z);
        }

        public void HandleInput()
        {
            if (_pc == null)
                return;
            if (Physics.Raycast(_pc.transform.position, Vector3.up, 1.5f))
            {
                Vector3 inputVector = new(_pc.MovementInput.x * _pc.PlayerSpeed * _pc.StrafeModifier * _pc.CrouchModifier,
                    0, _pc.MovementInput.y * _pc.PlayerSpeed * _pc.CrouchModifier);
                inputVector = _pc.CameraForward * inputVector;
                _pc.rb.AddForce(inputVector * Time.deltaTime, ForceMode.Force);
            }
            else if (!_pc.IsCrouching && _pc.MovementInput.magnitude == 0)
                _pc.SetState(new PlayerStandingState());
            else if (!_pc.IsCrouching)
                _pc.SetState(new PlayerWalkingState());
            else if (_pc.jumpPressed)
                _pc.SetState(new PlayerJumpingState());
            else
            {
                Vector3 inputVector = new(_pc.MovementInput.x * _pc.PlayerSpeed * _pc.StrafeModifier * _pc.CrouchModifier,
                    0, _pc.MovementInput.y * _pc.PlayerSpeed * _pc.CrouchModifier);
                if (inputVector.z < 0) inputVector.z *= _pc.ReverseModifier;
                else inputVector *= _pc.SprintSpeed;
                inputVector = _pc.CameraForward * inputVector;
                _pc.rb.AddForce(inputVector * Time.deltaTime, ForceMode.Force);
            }
        }
    }
}