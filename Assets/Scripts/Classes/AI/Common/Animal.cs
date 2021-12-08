namespace Hunter.AI.Common
{
    public abstract class Animal : IAnimal
    {
        protected IState State;
        
        public void Update()
        {
            State.Update();
        }
        public void ChangeState(IState state)
        {
            State = state;
        }
    }
}
