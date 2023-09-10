using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerStates { 
    public class IWallrunState : IPlayerState
    {
        PlayerController _pc;
        private float _maxWallRunTime = 4f;
        private float _wallRunTimer = 0f;
        private float _wallCheckDistance = 0.7f;
        private float _minWallrunHeight = 2f;
        private float _dismountForce = 10f;
        private RaycastHit leftWallHit;
        private RaycastHit RightWallHit;
        private bool WallLeft;
        private bool WallRight;
        public void Enter (PlayerController playerController)
        {
            _pc = playerController;
            _pc.rb.useGravity = false;
            _pc.rb.velocity = new Vector3(_pc.rb.velocity.x, 0, _pc.rb.velocity.z);
        }
        public void Exit ()
        {
            _pc.rb.useGravity = true;
        }
        public void HandleInput()
        {
            CheckForWall();
            if (_pc.jumpPressed)
            {
                Vector3 wallNormal = WallRight ? -_pc.transform.right : _pc.transform.right;
                _pc.rb.AddForce(wallNormal * _dismountForce, ForceMode.VelocityChange);
                _pc.SetState(new IJumpingState());
            }
            else if ((WallLeft || WallRight) && _pc.MovementInput.y > 0 && AboveGround())
            {
// TODO: wall run direction
                Vector3 wallNormal = WallRight ? RightWallHit.normal : leftWallHit.normal;
                Vector3 wallForward = Vector3.Cross(wallNormal, _pc.transform.up);
                _pc.rb.AddForce(wallForward * _pc.PlayerSpeed * _pc.SprintSpeed, ForceMode.VelocityChange);
            }
            else
                _pc.SetState(new IWalkingState());
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