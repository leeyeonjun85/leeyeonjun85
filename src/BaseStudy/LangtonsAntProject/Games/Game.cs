namespace LangtonsAntProject.Games
{
    public class Game
    {
        public int GenerationN { get; set; } = 0;
        public byte[,] Field { get; set; }
        public Ant[] Ants { get; set; }
        public int Size
        {
            get => Field.GetLength(0);
        }
        public Game(int size = 16)
        {
            Field = new byte[size, size];
            Ants = 
            [
                new Ant(i: size/2 + 1, j: size/2 + 1, direction: AntDirection.Left)
            ];
        }

        public byte[,] CalculateNextGeneration()
        {
            var newField = (byte[,])Field.Clone();

            for (int index = Ants.Length - 1; index >= 0; index--)
            {
                var ant = Ants[index];

                // Check if the ant is still within the field
                if (ant.row < 0 || ant.column < 0 || ant.column >= Size || ant.row >= Size)
                {
                    // TODO later you can act on ants going out of the field, 
                    // for now you just exclude them from processing
                    
                    continue;
                }

                byte oldVal = newField[ant.row, ant.column];
                int i = ant.row;
                int j = ant.column;
                byte newVal = ant.Act(oldVal);
                newField[i, j] = newVal;
                Field = newField;
            }

            return newField;
        }
    }
}
