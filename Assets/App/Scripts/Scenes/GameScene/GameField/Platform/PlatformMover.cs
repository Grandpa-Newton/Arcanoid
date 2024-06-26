﻿using App.Scripts.Libs.NodeArchitecture;
using App.Scripts.Scenes.GameScene.Game;

namespace App.Scripts.Scenes.GameScene.GameField.Platform
{
    public class PlatformMover : IContextFixedUpdate
    {
        private MouseInput _mouse;
        private PlatformView _platform;

        [GameInject]
        public void Construct(MouseInput mouseInput, PlatformView platformView)
        {
            _mouse = mouseInput;
            _platform = platformView;
        }
        
        public void OnFixedUpdate()
        {
            switch (_mouse.LeftButton)
            {
                case MouseInput.ButtonState.Down:
                case MouseInput.ButtonState.Press:
                    _platform.MoveToTarget(_mouse.Position.x);
                    break;
            }
        }
    }
}