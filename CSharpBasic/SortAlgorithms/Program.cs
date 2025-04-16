namespace SortAlgorithms {
    internal class Program {
        static void Main(string[] args) {
            int[] arr = { 7, 3, 3, 2, 8, 5, 0 };
            arr.BubbleSort();

            Console.Write("정렬됨 : ");
            for (int i = 0; i < arr.Length; i++) {
                Console.Write($"{arr[i]}, ");
            }
        }
    }
}
