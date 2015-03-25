using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shoot__n_Loot.Scenes
{
    static class SceneManager
    {
        private static Scene currentScene;
        internal static Scene CurrentScene { get { return currentScene; } set { currentScene = value; currentScene.OnResume(); } }

        internal static MainMenuScene mainMenuScene;
        internal static GameScene gameScene;
        internal static AboutScene aboutScene;
        internal static PauseScene pauseScene;
        internal static GameOverScene gameOverScene;

        public static void LoadAll()
        {
            gameScene = new GameScene();
            mainMenuScene = new MainMenuScene();
            aboutScene = new AboutScene();
            pauseScene = new PauseScene();
            gameOverScene = new GameOverScene();

            Map.Initialize(); //should be in gameScene but that fucks thigns up

            CurrentScene = mainMenuScene;
        }
    }
}
