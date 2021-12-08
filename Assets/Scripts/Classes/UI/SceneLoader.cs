using UnityEngine;
using UnityEngine.SceneManagement;

namespace Hunter.UI
{
    public class SceneLoader
    {
        public void LoadScene(int sceneId)
        {
            SceneManager.LoadScene(sceneId);
        }
        public void Quit()
        {
            Application.Quit();
        }
    }
}