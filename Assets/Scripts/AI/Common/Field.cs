namespace Hunter.AI.Common
{
    public class Field
    {
        public readonly float XLeftBorder;
        public readonly float XRightBorder;
        public readonly float YBotBorder;
        public readonly float YTopBorder;

        public Field(float xLeftBorder, float xRightBorder, float yBotBorder, float yTopBorder)
        {
            XLeftBorder = xLeftBorder;
            XRightBorder = xRightBorder;
            YBotBorder = yBotBorder;
            YTopBorder = yTopBorder;
        }
    }
}
