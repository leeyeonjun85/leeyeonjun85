namespace LangtonsAntProject.Games
{
    public class Ant
    {
        public int row { get; set; }
        public int column { get; set; }
        public AntDirection Direction { get; set; }

        public Ant(int i, int j, AntDirection direction)
        {
            row = i;
            column = j;
            Direction = direction;
        }

        public virtual byte Act(byte oldValue)
        {
            byte newValue;
            if (oldValue == 0)
            {
                newValue = 1;
            }
            else
            {
                newValue = 0;
            }
            Turn(oldValue);
            Move();
            return newValue;
        }

        public void Turn(byte oldValue)
        {
            if (oldValue == 0)
            {
                RotateRight();
            }
            else
            {
                RotateLeft();
            }
        }


        protected void Move()
        {
            if (AntDirection.Up == Direction) row--;
            if (AntDirection.Right == Direction) column++;
            if (AntDirection.Down == Direction) row++;
            if (AntDirection.Left == Direction) column--;
        }

        public void RotateRight()
        {
            Direction = (AntDirection)(((int)Direction + 1) % 4);
        }

        public void RotateLeft()
        {
            Direction = (AntDirection)((int)Direction == 0 ? 3 : (int)Direction - 1);
        }
    }
}
