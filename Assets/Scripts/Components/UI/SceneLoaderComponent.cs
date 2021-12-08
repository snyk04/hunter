using UnityEngine;

namespace Hunter.UI
{
    public class SceneLoaderComponent : MonoBehaviour
    {
        private SceneLoader _sceneLoader;

        private void Awake()
        {
            _sceneLoader = new SceneLoader();
        }
        
        public void LoadScene(int sceneId)
        {
            _sceneLoader.LoadScene(sceneId);
        }
        public void Quit()
        {
            _sceneLoader.Quit();
        }
    }
}
