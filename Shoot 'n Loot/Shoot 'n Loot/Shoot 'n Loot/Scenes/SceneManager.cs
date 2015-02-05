using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shoot__n_Loot.Scenes
{
    static class SceneManager
    {
        internal static Scene currentScene;

        internal static MainMenuScene mainMenuScene;
        internal static GameScene gameScene;
        internal static AboutScene aboutScene;
        internal static PauseScene pauseScene;

        public static void LoadAll()
        {
            gameScene = new GameScene();
            mainMenuScene = new MainMenuScene();
            aboutScene = new AboutScene();
            pauseScene = new PauseScene();

            currentScene = mainMenuScene;
        }
    }
}
