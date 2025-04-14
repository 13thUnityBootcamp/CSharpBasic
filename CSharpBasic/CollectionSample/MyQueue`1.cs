using System.Collections;

namespace CollectionsSample {
    /// <summary>
    /// 선입선출, 순환배열로 데이터 관리
    /// </summary>
    class MyQueue<T> : IEnumerable<T> {
        public MyQueue(int capacity) {
            _data = new T[capacity];
            _last = -1;
        }

        /// <summary>
        /// 용량 (데이터배열크기)
        /// 용량 변경시 first의 item 을 가장 앞에 배치하고 정렬하며 용량 증가
        /// </summary>
        public int Capacity {
            get => _data.Length;
            set {
                if (_count > value)
                    throw new Exception("용량은 현재 아이템 수보다 작을수 없습니다.");

                T[] tmp = new T[value];

                for (int i = 0; i < _count; i++) {
                    tmp[i] = _data[(_first + i) % _data.Length]; // 순환배열이므로 인덱스 초과시 처음인덱스부터 가져오기위함
                }

                _data = tmp;
                _first = 0;
                _last = _count - 1;
            }
        }

        /// <summary>
        /// 아이템 수
        /// </summary>
        public int Count => _count;

        T[] _data;
        int _first;
        int _last;
        int _count;


        /// <summary>
        /// 아이템 추가
        /// 용량 부족할시 두배 키움
        /// 순환배열사용으로 추가한 아이템의 인덱스를 순환시킴.
        /// </summary>
        /// <param name="item"> 추가할 아이템 </param>
        public void Enqueue(T item) {
            if (Capacity == _count)
                Capacity *= 2;

            _last = (_last + 1) % _data.Length;
            _data[_last] = item;
            _count++;
        }

        /// <summary>
        /// 아이템 삭제 
        /// 가장 앞 아이템 제거 후 반환
        /// </summary>
        /// <returns> 가장 앞에 있었던 아이템 </returns>
        /// <exception cref="Exception"> 아이템 없음 </exception>
        public T Dequeue() {
            if (_count == 0)
                throw new Exception("대기열에 아무것도 없으므로 가장 앞을 제거할수 없음");

            T item = _data[_first];
            _data[_first] = default;
            _count--;

            if (_count > 0) {
                _first = (_first + 1) % _data.Length; // 가장 앞 아이템이 빠졌으므로 다음 아이템이 가장앞 아이템이 되어야함.
            }
            else {
                _first = 0;
                _last = -1;
            }

            return item;
        }

        /// <summary>
        /// 대기열순위 가장 앞 아이템 반환
        /// </summary>
        /// <returns> 최우선순위 아이템 </returns>
        /// <exception cref="Exception"> 아이템 없음 </exception>
        public T Peek() {
            if (_count == 0)
                throw new Exception("대기열에 아무것도 없으므로 가장 앞을 읽을수없음");

            return _data[_first];
        }

        public IEnumerator<T> GetEnumerator() {
            return new Enumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        public struct Enumerator : IEnumerator<T> {
            public Enumerator(MyQueue<T> queue) {
                _queue = queue;
            }

            public T Current => _current;

            object IEnumerator.Current => Current;

            MyQueue<T> _queue;
            int _index;
            T _current;


            public bool MoveNext() {
                if (_index < _queue._count) {
                    _current = _queue._data[(_queue._first + _index) % _queue._data.Length];
                    _index++;
                    return true;
                }

                return false;
            }

            public void Reset() {
                _index = 0;
                _current = default;
            }

            public void Dispose() {
            }
        }
    }
}