﻿using System;
using App.Scripts.Libs.NodeArchitecture;
using App.Scripts.Scenes.AllScenes.ProjectContext;
using App.Scripts.Scenes.AllScenes.ProjectContext.Packs;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace App.Scripts.Scenes.GameScene.Game
{
    [RequireComponent(typeof(GameContext))]
    [InfoBox("$GameState")]
    public class GameManager : MonoBehaviour
    {
        public GameState GameState { get; private set; }

        [SerializeField]
        private GameContext context;
        private ContextNode _root;
        
        [SerializeField]
        private bool autoRun = true;

        private PacksController _packsController;
        private SceneSwitcher _sceneSwitcher;

        public Action OnSkipLevel;
        public Action OnGameWin, OnGameLose;
         
        private void Start()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 60;
            
            context.RegisterInstance(this);
            _root = ProjectContext.Instance;
            _root.Construct();
            _root.AddChild(context);
            
            GameState = GameState.Off;

            if (autoRun)
            {
                ConstructGame();
                InitGame();
                StartGame();
            }
        }

        [GameInject]
        public void Construct(PacksController packsController, SceneSwitcher sceneSwitcher)
        {
            _packsController = packsController;
            _sceneSwitcher = sceneSwitcher;
        }

        public void EndGame(LevelResult result)
        {
            _packsController.SaveLevelResult(result);
            if (result == LevelResult.Win) OnGameWin?.Invoke();
            else OnGameLose?.Invoke();
            //_sceneSwitcher.LoadSceneAsync("PacksScene").Forget();
        }

        public void SkipLevel()
        {
            OnSkipLevel?.Invoke();
        }
        
        public void Secede()
        {
            _root.RemoveChild(context);
        }

        private void Update()
        {
            _root.OnUpdate();
        }

        private void FixedUpdate()
        {
            _root.OnFixedUpdate();
        }

        private void LateUpdate()
        {
            _root.OnLateUpdate();
        }
        
        [ContextMenu("Inject")]
        public void ConstructGame()
        {
            _root.SendEvent<GameInject>();
        }

        [ContextMenu("Init")]
        public void InitGame()
        {
            _root.SendEvent<GameInit>();
        }

        [ContextMenu("Start")]
        public void StartGame()
        {
            _root.SendEvent<GameStart>();
            GameState = GameState.Playing;
        }

        [ContextMenu("Pause")]
        public void PauseGame()
        {
            _root.SendEvent<GamePause>();
            GameState = GameState.Paused;
        }

        [ContextMenu("Resume")]
        public void ResumeGame()
        {
            _root.SendEvent<GameResume>();
            GameState = GameState.Playing;
        }

        [ContextMenu("Finish")]
        public void FinishGame()
        {
            _root.SendEvent<GameFinish>();
            GameState = GameState.Finished;
        }
    }
}