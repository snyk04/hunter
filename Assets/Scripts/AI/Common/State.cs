namespace Hunter.AI.Common
{
    public abstract class State : IState
    {
        protected readonly AnimalInfo AnimalInfo;
        
        protected State(AnimalInfo animalInfo)
        {
            AnimalInfo = animalInfo;
        }
        
        public abstract void Update();

        protected void ChangeAnimalState(IState state)
        {
            AnimalInfo.Animal.ChangeState(state);
        }
    }
}
