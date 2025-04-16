namespace CollectionsSample {
    class MyHashSet<T> {
        const int DEFAULT_SIZE = 5;
        T[] _data = new T[DEFAULT_SIZE];

        public bool Add(T item) {
            int hashCode = Hash(item);

            // 중복값 허용 X
            if (item.Equals(_data[hashCode]))
                return false;

            _data[hashCode] = item;
            return true;
        }

        public bool Contains(T item) {
            int hashCode = Hash(item);
            T searched = _data[hashCode];
            return searched.Equals(item);
        }

        public bool Remove(T item) {
            int hashCode = Hash(item);

            if (_data[hashCode].Equals(item)) {
                _data[hashCode] = default;
                return true;
            }

            return false;
        }

        /// <summary>
        /// 문자열로 변환후 각 문자의 아스키값을 더한 후 크기로 나머지하는 간단한 해시함수
        /// </summary>
        int Hash(T value) {
            string str = value.ToString();
            int sum = 0;

            for (int i = 0; i < str.Length; i++) {
                sum += str[i];
            }

            sum %= DEFAULT_SIZE;
            return sum;
        }
    }
}