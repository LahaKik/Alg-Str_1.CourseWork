namespace CourseWork
{
    internal class Program
    {
        private static int MainMenu()
        {
            bool Success = false;
            int rezult = -1;
            while (!Success)
            {
                Console.WriteLine("Select menu item:");
                Console.WriteLine("1 - Load or reload graph from file");
                Console.WriteLine("2 - Create a minamum frame of graph");
                Console.WriteLine("3 - Present graph as adjacency list");
                Console.WriteLine("4 - Present graph as adjacency matrix");
                Console.WriteLine("9 - Exit");
                Console.WriteLine("Your choice:");

                string? inp = Console.ReadLine();
                Console.Clear();

                if (int.TryParse(inp, out rezult))
                {
                    if (rezult == 9 || (rezult > 0 && rezult < 5))
                    {
                        Success = true;
                    }
                    else
                    {
                        Console.WriteLine("Incorrect input!");
                    }
                }
                else
                {
                    Console.WriteLine("Incorrect input!");
                }
            }

            Console.Clear();
            return rezult;
        }
        static void Main(string[] args)
        {

            Graph? g = null;

            bool Exit = false;
            while (!Exit)
            {
                int choice = MainMenu();

                switch (choice)
                {
                    case 1:
                        try
                        {
                            g = new Graph();
                            Console.WriteLine(g.ToString());
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                            Console.WriteLine("Check the file!");
                        }
                        break;
                    case 2:
                        g?.MinimumFrame();
                        break;
                    case 3:
                        g?.PresentAsAdjacencyList();
                        break;
                    case 4:
                        Console.WriteLine(g?.ToString());
                        break;

                    case 9:
                        Exit = true;
                        break;

                    default:
                        break;
                }
            }

        }
    }
}
