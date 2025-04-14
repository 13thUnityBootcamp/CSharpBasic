
using System.Collections;

namespace CollectionsSample {
    class MyStack<T> : IEnumerable<T> {
        public MyStack(int capacity) {
            _data = new T[capacity];
        }

        public T this[int index] {
            get {
                if (index >= _count || index < 0)
                    throw new IndexOutOfRangeException();

                return _data[index];
            }
            set {
                if (index >= _count || index < 0)
                    throw new IndexOutOfRangeException();

                _data[index] = value;
            }
        }

        public int Count => _count;

        public int Capacity {
            get => _data.Length;
            set {
                if (_count > value)
                    throw new ArgumentException("현재 아이템 수 보다 작은 용량으로 설정할수 없습니다.");

                T[] temp = new T[value];

                for (int i = 0; i < _count; i++) {
                    temp[i] = _data[i];
                }

                _data = temp;
            }
        }

        private T[] _data;
        private int _count;


        public void Push(T item) {
            // 용량이 부족할때
            if (_count == _data.Length) {
                T[] temp = new T[_count * 2]; // 크기 2배짜리 배열 생성

                // 기존 데이터 복사
                for (int i = 0; i < _count; i++) {
                    temp[i] = _data[i];
                }

                _data = temp;
            }

            _data[_count++] = item;
        }

        public T Pop() {
            if(_count == 0)
                throw new IndexOutOfRangeException();

            _count--;
            T item = _data[_count];
            _data[_count] = default;
            return item;
        }
        
        public T Peek() {
            if(_count == 0)
                throw new IndexOutOfRangeException();

            return _data[_count - 1];
        }

        public IEnumerator<T> GetEnumerator() {
            return new Enumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        struct Enumerator : IEnumerator<T> {
            public Enumerator(MyStack<T> list) {
                _list = list;
                _index = 0;
                _current = default(T);
            }

            public T Current => _current;

            object IEnumerator.Current => Current;

            MyStack<T> _list;
            int _index;
            T _current;

            public bool MoveNext() {
                if (_index < _list.Count) {
                    _current = _list._data[_index];
                    _index++;
                    return true;
                }

                _current = default(T);
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