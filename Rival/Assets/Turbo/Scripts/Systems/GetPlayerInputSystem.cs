using System;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace TMG.SpaceShooter
{
    public partial class GetPlayerInputSystem : SystemBase
    {
        private PlayerInputKeys _playerInputKeys;

        private Camera _mainCamera;

        protected override void OnStartRunning()
        {
            _playerInputKeys = GetSingleton<PlayerInputKeys>();
            _mainCamera = Camera.main;
        }

        protected override void OnUpdate()
        {
            var newPlayerInput = GetPlayerMoveInput();
            SetSingleton(newPlayerInput);

            var curWorldMousePos = GetWorldMousePosition();
            SetSingleton(curWorldMousePos);
        }

        private PlayerMoveInput GetPlayerMoveInput()
        {
            var horizontalMovement = 0f;
            if (Input.GetKey(_playerInputKeys.RightKey))
            {
                horizontalMovement = 1f;
            }
            else if (Input.GetKey(_playerInputKeys.LeftKey))
            {
                horizontalMovement = -1f;
            }

            var verticalMovement = 0f;
            if (Input.GetKey(_playerInputKeys.UpKey))
            {
                verticalMovement = 1f;
            }
            else if (Input.GetKey(_playerInputKeys.DownKey))
            {
                verticalMovement = -1f;
            }

            var playerMovement = new float3
            {
                x = horizontalMovement,
                y = 0f,
                z = verticalMovement,
            };

            if (math.lengthsq(playerMovement) > 1f)
            {
                playerMovement = math.normalize(playerMovement);
            }

            return new PlayerMoveInput
            {
                Value = playerMovement,
            };

        }

        private WorldMousePosition GetWorldMousePosition()
		{
            var mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _mainCamera.transform.position.y);

            var worldMousePos = _mainCamera.ScreenToWorldPoint(mousePos);

            return new WorldMousePosition
            {
                Value = worldMousePos
            };
		}
    }
}
