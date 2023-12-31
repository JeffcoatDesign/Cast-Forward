using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

namespace PlayerStates { 
    public class PlayerWallrunState : IPlayerState
    {
        PlayerController _pc;
        private float _wallRunTimer = 0f;
        private float _wallCheckDistance = 1.2f;
        private float _minWallrunHeight = 1f;
        private float _dismountForce = 10f;
        private RaycastHit leftWallHit;
        private RaycastHit RightWallHit;
        private bool WallLeft;
        private bool WallRight;
        public void Enter (PlayerController playerController)
        {
            _pc = playerController;
            _pc.rb.useGravity = false;
            _wallRunTimer = Time.time;
            _pc.rb.velocity = new Vector3(_pc.rb.velocity.x, 0, _pc.rb.velocity.z);
        }
        public void Exit ()
        {
            _pc.rb.useGravity = true;
            _pc.PlayerVCamController.SetDutch(0);
        }
        public void HandleInput()
        {
            CheckForWall();
            if (_pc.jumpPressed)
            {
                Vector3 wallNormal = WallRight ? RightWallHit.normal : leftWallHit.normal;
                _pc.rb.AddForce(wallNormal * _dismountForce, ForceMode.VelocityChange);
                _pc.SetState(new PlayerDoubleJumpState());
            }
            else if ((WallLeft || WallRight) && _pc.MovementInput.y > 0 && AboveGround() && Time.time - _wallRunTimer < _pc.MaxWallRunTime)
            {
                _pc.PlayerVCamController.SetDutch(WallRight ? _pc.WallDutchAngle : -_pc.WallDutchAngle);

                Vector3 wallNormal = WallRight ? RightWallHit.normal : leftWallHit.normal;
                Vector3 wallForward = Vector3.Cross(wallNormal, _pc.transform.up);

                if ((_pc.transform.forward - wallForward).magnitude > (_pc.transform.forward - -wallForward).magnitude)
                    wallForward = -wallForward;

                _pc.rb.AddForce(wallForward * _pc.PlayerSpeed * _pc.SprintSpeed, ForceMode.Force);
            }
            else
            {
                Vector3 wallNormal = WallRight ? RightWallHit.normal : leftWallHit.normal;
                _pc.rb.AddForce(wallNormal * _dismountForce, ForceMode.VelocityChange);
                _pc.SetState(new PlayerFallState());
            }
        }

        private void CheckForWall ()
        {
            WallLeft = Physics.Raycast(_pc.transform.position, -_pc.transform.right, out leftWallHit, _wallCheckDistance, _pc.WhatIsWall);
            WallRight = Physics.Raycast(_pc.transform.position, _pc.transform.right, out RightWallHit, _wallCheckDistance, _pc.WhatIsWall);
        }

        private bool AboveGround ()
        {
            return !Physics.Raycast(_pc.transform.position, Vector3.down, _minWallrunHeight, _pc.WhatIsGround);
        }
    }
}