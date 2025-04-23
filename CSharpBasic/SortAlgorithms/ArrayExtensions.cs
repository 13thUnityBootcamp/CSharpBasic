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

        /// <summary>
        /// 현재 탐색중인 인덱싀 앖을 Key로 두고
        /// 현재 탐색중인 인덱스보다 앞에 key값 보다 큰 값들이 있다면 전부 오른쪽으로 한 칸씩 당김
        /// 더 이상 밀 수 없을 때 그 위치에 Key값을 쓴다
        /// </summary>
        /// <param name="arr"></param>
        public static void InsertionSort(this int[] arr)
        {
            int i, j, key;

            for(i = 1; i < arr.Length; i++) {
                key = arr[i];

                for(j = i - 1; j >= 0; j--) {
                    if (arr[j] > key) {
                        arr[j + 1] = arr[j];
                    }
                    else {
                        break;
                    }
                }

                arr[j + 1] = key; // for 루프 마지막에 j-- 하기 때문에 다시 +1해서 쓴거임
            }
        }

        public static void MergeSort(this int[] arr) {
            int length = arr.Length;

            // 병합 단위
            for(int mergeSize =1; mergeSize < length; mergeSize *= 2) {
                // 병합 범위
                for(int start =0; start < length; start += mergeSize * 2) {
                    // 왼쪽 파티션 : start ~mergeSize - 1
                    // 오른쪽 파티션 : start ~ 2 * mergeSize - 1
                    int mid = Math.Min(start + mergeSize - 1, length -1);
                    int end = Math.Min(start + 2 * mergeSize - 1, length - 1);

                    Merge(arr, start, mid, end);
                }
            }
        }

        public static void RecursiveMergeSort(this int[] arr) {
            RecursiveMergeSort(arr, 0, arr.Length - 1);
        }

        private static void RecursiveMergeSort(this int[] arr, int start, int end) {
            int mid = (start + end) / 2; // end - 1 + (start - end + 1) / 2
            RecursiveMergeSort(arr, start, mid);
            RecursiveMergeSort(arr, mid + 1, end);
        }

        private static void Merge(int[] arr, int start, int mid, int end) {
            int length1 = mid - start + 1; // start ~ mid (part1 길이)
            int length2 = end - (mid + 1) + 1; // mid + 1 ~ end (part2 길이)
            int[] copy1 = new int[length1]; // part1(왼쪽) 카피
            int[] copy2 = new int[length2]; // part2(오른쪽) 카피

            for (int i = 0; i < length1; i++)
                copy1[i] = arr[i + start];

            for (int i = 0; i < length2; i++)
                copy2[i] = arr[i + mid + 1];

            int part1 = 0;
            int part2 = 0;
            int index = start; // 현재 정렬하려는 위치

            // 두 파트 중 하나라도 다 소진할 때까지 반복하며 더 작은 것을 채택하여 정렬
            while (part1 < length1 && part2 < length2) {
                if (copy1[part1] <= copy2[part2])
                    arr[index++] = copy1[part1++];
                else
                    arr[index++] = copy2[part2++];
            }

            // part1만 남았다면 index부터 쭉 이어서 붙여넣는다
            while (part1 < length1)
                arr[index++] = copy1[part1++];
        }

        public static void RecursiveQuickSort(this int[] arr) {
            RecursiveQuickSort(arr, 0, arr.Length - 1);
        }

        private static void RecursiveQuickSort(int[] arr, int start, int end) {
            if (start < end) {
                // Pivot을 기준으로 좌우로 나눌 인덱스를 가져옴  
                int p = QuickSortPartition(arr, start, end);

                // 왼쪽 파티션 정렬  
                RecursiveQuickSort(arr, start, p - 1);

                // 오른쪽 파티션 정렬  
                RecursiveQuickSort(arr, p + 1, end);
            }
        }

        /// <summary>  
        /// Quick 정렬 로직에서 Pivot을 선정하여 Pivot 값 기준 정렬을 수행하고  
        /// 고정된 인덱스를 반환해서 상위 함수에서 고정된 값을 기준으로 좌, 우 분할 로직을 수행할 수 있도록 함  
        /// </summary>  
        /// <returns> 이번 정렬에서 고정될 배열의 인덱스 </returns>  
        private static int QuickSortPartition(int[] arr, int start, int end) {
            int mid = start + ((end - start) >> 1);
            int pivot = arr[mid];

            while (true) {
                while (arr[start] < pivot) 
                    start++;

                while (arr[end] > pivot)
                    end--;

                if (start < end) {
                    arr.Swap(start, end);
                    start++;
                    end--;
                }
                else {
                    return end;
                }
            }
        }

        public static void HeapSort(this int[] arr) {
            HeapifyBottom(arr);
            InverseHeapify(arr);
        }

        private static void HeapifyBottom(int[] arr){
            int current = arr.Length - 1;

            while (current >= 0) {
                SIFTDown(arr,current--, arr.Length - 1);
            }
        }

        private static void InverseHeapify(int[] arr) {
            int end = arr.Length - 1;

            while (end > 0) {
                Swap(arr, 0, end--);
                SIFTDown(arr, 0, end);
            }
        }

         private static void SIFTDown(int[] arr, int current, int end)
        {
            int leftChild = current * 2 + 1;

            // 더이상 아래로 스왑이 불가능할때까지 반복
            while (leftChild <= end)
            {
                int rightChild = leftChild + 1;
                int priorityChild = leftChild;

                // 오른쪽 자식이 있으면서, 오른쪽자식이 왼쪽자식보다 크면 우선순위 바꿈
                if (rightChild <= end &&
                    arr[rightChild] > arr[leftChild])
                {
                    priorityChild = rightChild;
                }

                // 자식의 우선순위가 더 높은지 확인 후 스왑
                if (arr[priorityChild] > arr[current])
                {
                    Swap(arr, priorityChild, current);
                    current = priorityChild;
                    leftChild = current * 2 + 1;
                }
                else
                {
                    break;
                }
            }
        }


        static void Swap(this int[] arr, int index1, int index2) {
            int tmp = arr[index1];
            arr[index1] = arr[index2];
            arr[index2] = tmp;
        }
    }
}
