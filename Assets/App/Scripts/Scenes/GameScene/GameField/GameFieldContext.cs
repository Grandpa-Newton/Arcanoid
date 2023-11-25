﻿using App.Scripts.Libs.NodeArchitecture;
using App.Scripts.Scenes.GameScene.GameField.Ball;
using App.Scripts.Scenes.GameScene.GameField.Block;
using App.Scripts.Scenes.GameScene.GameField.Level;
using App.Scripts.Scenes.GameScene.GameField.Platform;
using UnityEngine;

namespace App.Scripts.Scenes.GameScene.GameField
{
    public class GameFieldContext : ContextNode
    {
        [SerializeField] private GameFieldManager gameFieldManager;
        [SerializeField] private PlatformView platformView;
        private LevelLoader _levelLoader = new();
        private LevelView _levelView = new();
        private BlockBehaviourHandler _blockBehaviourHandler = new();
        private PlatformMover _platformMover = new();
        private BallsController _ballsController = new();
        private BallCollisionController _ballCollisionController = new();
        
        protected override void OnConstruct()
        {
            RegisterInstance(_levelLoader);
            RegisterInstance(_levelView);
            RegisterInstance(_blockBehaviourHandler);
            RegisterInstance(platformView);
            RegisterInstance(_platformMover);
            RegisterInstance(_ballsController);
            RegisterInstance(_ballCollisionController);
            RegisterInstance(gameFieldManager);
        }
    }
}