using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerStates {
    public class PlayerSlideState : IPlayerState
    {
        private PlayerController _pc;
        private float _defYScale;
        private float _slideTime = 1f;

        public void Enter(PlayerController playerController)
        {
            _pc = playerController;
            _defYScale = _pc.transform.localScale.y;
            _pc.transform.localScale = new Vector3(_pc.transform.localScale.x, 0.5f, _pc.transform.localScale.z);
            _pc.rb.AddForce(Vector3.down * 5, ForceMode.Impulse);
            _pc.StartCoroutine(Slide());
        }

        public void Exit()
        {
            _pc.transform.localScale = new Vector3(_pc.transform.localScale.x, _defYScale, _pc.transform.localScale.z);
        }

        public void HandleInput()
        {
            if (_pc == null)
                return;
            bool hasNoVelocity = _pc.rb.velocity.magnitude <= 0.5f;
            if (Physics.Raycast(_pc.transform.position, Vector3.up, 1.5f))
                _pc.rb.AddForce(_pc.transform.forward * 5, ForceMode.Impulse);
            else if (!_pc.IsCrouching && _pc.MovementInput.magnitude == 0 && hasNoVelocity) {
                _pc.StopAllCoroutines();
                _pc.SetState(new PlayerStandingState());
            }
            else if (!_pc.IsCrouching && hasNoVelocity) {
                _pc.StopAllCoroutines();
                _pc.SetState(new PlayerWalkingState());
            }
            else if (_pc.jumpPressed) {
                _pc.StopAllCoroutines();
                _pc.SetState(new PlayerJumpingState());
            }
            else if (hasNoVelocity)
                _pc.SetState(new PlayerCrouchState());
        }

        private IEnumerator Slide ()
        {
            float startTime = Time.time;
            while (Time.time - startTime < _slideTime)
            {
                float modifier = Mathf.Lerp(5, 1, (Time.time - startTime) / _slideTime);
                _pc.rb.AddForce(_pc.transform.forward * modifier, ForceMode.Impulse);
                yield return new WaitForFixedUpdate();
            }
            yield return null;
        }
    }
}