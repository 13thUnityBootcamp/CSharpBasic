namespace PracticeOOP {
    internal class Program {
        static void Main(string[] args) {
            int a = 1;
            object obj = 1; // Boxing
            Object obj2 = new Object();
            a = (int)obj; // Unboxing
            // Boxing Unboxing 은 연산비용이 높기때문에 지양해야한다.

            GamePlayManager gamePlayManager = new GamePlayManager();

            Console.WriteLine("Press any key to start the game ...");
            Console.ReadKey();
            Console.Clear();
            gamePlayManager.PlayGame();
        }
    }
}