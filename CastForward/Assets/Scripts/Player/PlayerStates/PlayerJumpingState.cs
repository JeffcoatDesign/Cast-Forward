using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerStates {
    public class PlayerJumpingState : IPlayerState
    {
        PlayerController _pc;
        float _stateStartTime;
        float _JumpBuffer = 0.2f;
        private float _wallCheckDistance = 1.2f;
        private float _minWallrunHeight = 1f;
        private bool WallLeft;
        private bool WallRight;
        private bool _jumpHasReleased = false;
        public void Enter(PlayerController playerController)
        {
            _pc = playerController;
            _stateStartTime = Time.time;
            _pc.rb.AddForce(Vector3.up * _pc.JumpPower);
            _pc.ToggleCrouch(false);
        }

        public void Exit()
        {

        }

        public void HandleInput ()
        {
            CheckForWall();
            Vector3 inputVector = new(_pc.MovementInput.x * _pc.PlayerSpeed * _pc.StrafeModifier,
                       0, _pc.MovementInput.y * _pc.PlayerSpeed);
            if (inputVector.z < 0) inputVector.z *= _pc.ReverseModifier;
            inputVector *= _pc.SprintSpeed;
            inputVector = _pc.CameraForward * inputVector;
            _pc.rb.AddForce(inputVector, ForceMode.Force);

            if (!_pc.jumpPressed) _jumpHasReleased = true;

            bool bufferedJump = Time.time - _stateStartTime > _JumpBuffer;

            if (_pc.jumpPressed && bufferedJump && _jumpHasReleased)
                _pc.SetState(new PlayerDoubleJumpState());
            else if (_pc.isGrounded && bufferedJump)
            {
                if (_pc.IsCrouching)
                    _pc.SetState(new PlayerSlideState());
                else
                    _pc.SetState(new PlayerWalkingState());
            }
            else if ((WallLeft || WallRight) && _pc.MovementInput.y > 0 && AboveGround() && bufferedJump)
                _pc.SetState(new PlayerWallrunState());
        }
        private void CheckForWall()
        {
            WallLeft = Physics.Raycast(_pc.transform.position, -_pc.transform.right, _wallCheckDistance, _pc.WhatIsWall);
            WallRight = Physics.Raycast(_pc.transform.position, _pc.transform.right, _wallCheckDistance, _pc.WhatIsWall);
        }

        private bool AboveGround()
        {
            return !Physics.Raycast(_pc.transform.position, Vector3.down, _minWallrunHeight, _pc.WhatIsGround);
        }
    }
}