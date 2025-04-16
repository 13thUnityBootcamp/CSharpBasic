namespace SortAlgorithms {
    static class ArrayExtensions {
        public static void BubbleSort(this int[] arr) {
            // 패싱 반복
            for (int i = 0; i < arr.Length - 1; i++) {
                // 패싱. 패싱 반복 횟수만큼 끝자리 고정되므로 탐색 갯수 줄어듬
                for (int j = 0; j < arr.Length - 1; j++) {
                    if (arr[j] > arr[j + 1])
                        arr.Swap(j, j + 1);
                }
            }
        }

        /// <summary>
        /// 현재 탐색 중인 인덱스 뒤로 가장 작은 값을 가지는 인덱스를 찾아서 걔랑 스왑
        /// 특징 : 힌 번 패싱할 때마다 가장 작은 수가 고정
        /// </summary>
        /// <param name="arr"></param>
        public static void SelectionSort(this int[] arr) {
            int minIdx = 0;

            for (int i = 0; i < arr.Length - 1; i++) {
                for (int j = i +1; j < arr.Length; j++) {
                    if (arr[j] < arr[minIdx])
                        minIdx = j;
                }

                arr.Swap(i, minIdx);
            }
        }


        static void Swap(this int[] arr, int index1, int index2) {
            int tmp = arr[index1];
            arr[index1] = arr[index2];
            arr[index2] = tmp;
        }
    }
}
