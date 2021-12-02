using Hunter.Creatures;
using UnityEngine.UI;

namespace Hunter.UI
{
    public class AmmoCounter
    {
        private readonly Text _counterText;
        private readonly Shooter _shooter;

        public AmmoCounter(Text counterText, Shooter shooter)
        {
            _counterText = counterText;
            _shooter = shooter;
            
            Update();

            _shooter.OnShot += Update;
            _shooter.OnReload += Update;
        }

        private void Update()
        {
            _counterText.text = $"{_shooter.AmountOfBulletsInMagazine}/{_shooter.AmountOfBulletsInBackpack}";
        }
    }
}
