using System;

// ƒRƒ“ƒ\[ƒ‹‚Å“®‚­
class MazeGenerator
{
    private int width, height;
    private int[,] maze;

    public MazeGenerator(int width, int height)
    {
        this.width = width % 2 == 0 ? width + 1 : width; // •‚ğŠï”‚É
        this.height = height % 2 == 0 ? height + 1 : height; // ‚‚³‚ğŠï”‚É
        maze = new int[this.width, this.height];
        GenerateMaze();
    }

    private void GenerateMaze()
    {
        // ‰Šú‰»F•Ç‚Æ’Ê˜H
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (x % 2 != 0 && y % 2 != 0)
                {
                    maze[x, y] = 0; // ’Ê˜H
                }
                else
                {
                    maze[x, y] = 1; // •Ç
                }
            }
        }

        // –_“|‚µ–@‚É‚æ‚é–À˜H¶¬
        Random rand = new Random();
        for (int y = 2; y < height - 2; y += 2)
        {
            for (int x = 2; x < width - 2; x += 2)
            {
                int direction = rand.Next(4);
                switch (direction)
                {
                    case 0: // ã
                        maze[x, y - 1] = 0;
                        break;
                    case 1: // ‰E
                        maze[x + 1, y] = 0;
                        break;
                    case 2: // ‰º
                        maze[x, y + 1] = 0;
                        break;
                    case 3: // ¶
                        maze[x - 1, y] = 0;
                        break;
                }
            }
        }
    }

    public void PrintMaze()
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Console.Write(maze[x, y] == 1 ? "¡" : " ");
            }
            Console.WriteLine();
        }
    }

    public void Main()
    {
        MazeGenerator maze = new MazeGenerator(15, 15);
        maze.PrintMaze();
    }
}
