using LangtonsAntProject.Games;

namespace LangtonsAntProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int size = 16;
            Game game = new(size);

            int turn = 0;
            while (true)
            {
                turn++;
                Console.WriteLine($"LangtonsAnts Turn : {turn}");
                Print(game);
                game.CalculateNextGeneration();
            }
        }

        static void Print(Game game)
        {
            for (int i = 0; i < game.Field.GetLength(0); i++)
            {
                for (int j = 0; j < game.Field.GetLength(1); j++)
                {
                    string fieldChar = "□ ";

                    // If the ant is at the cell, display ant direction instead of color value
                    Ant? ant = game.Ants.FirstOrDefault(a => (i == a.row) && (j == a.column));

                    if (ant != null)
                    {
                        // Draw one of the ants
                        switch (ant.Direction)
                        {
                            case AntDirection.Up:
                                fieldChar = "↑ ";
                                break;
                            case AntDirection.Right:
                                fieldChar = "→ ";
                                break;
                            case AntDirection.Down:
                                fieldChar = "↓ ";
                                break;
                            case AntDirection.Left:
                                fieldChar = "← ";
                                break;
                        }
                    }
                    else
                    {
                        fieldChar = game.Field[i, j] == 0 ? "□ " : "■ ";
                    }
                    Console.Write($"{fieldChar}");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
