using System.Diagnostics;

namespace SortAlgorithms {
    internal class Program {
        static void Main(string[] args) {
            Random random = new Random();
            int[] arr = Enumerable.Repeat(0, 100000) // 0 아이템 10만개짜리 자료를 생성
                                  .Select(x=> random.Next(0, 100000)) // 자료전체 순회하면서 x에 대해 0~10만 사이 값 난수를 반환한 값으로 새 자료 생성
                                  .ToArray(); // 배열로 변경
            Stopwatch stopwatch = Stopwatch.StartNew();

            // arr.BubbleSort(); // 10만개, 난수범위 0~10만에서 39500ms
            // arr.SelectionSort(); // 10만개, 난수번위 0 ~10만 에서 119000ms
            // arr.InsertionSort(); // 10만개, 난수범위 0~10만 에서 7450ms
            // arr.RecursiveMergeSort(); // 1000만개, 난수범위 0~1000만에서 3400ms
            arr.MergeSort(); // 1000만개, 난수범위 0~1000만 

            // C#의 Sort는
            // 퀵정렬이 기본이나 파티션수가 적을떄, 삽입정렬, 파티션수가 2log N 이상 많아질때 합정렬로 전환

            stopwatch.Stop();
            Console.WriteLine($"정렬 걸린 시간 : {stopwatch.ElapsedMilliseconds} ms");

            //Console.Write("정렬됨 : ");
            //for (int i = 0; i < arr.Length; i++) {
            //    Console.Write($"{arr[i]}, ");
            //}
        }
    }
}
