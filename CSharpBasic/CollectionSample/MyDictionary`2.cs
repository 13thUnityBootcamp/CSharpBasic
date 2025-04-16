namespace CollectionsSample {
    struct KeyValuePair<TKey, TValue> {
        public KeyValuePair(TKey key, TValue value) {
            Key = key;
            Value = value;
        }

        public TKey Key;
        public TValue Value;
    }


    class MyDictionary<TKey, TValue> {
        const int DEFAULT_SIZE = 5;
        KeyValuePair<TKey, TValue>[] _data = new KeyValuePair<TKey, TValue>[DEFAULT_SIZE];

        public bool Add(TKey key, TValue value) {
            int hashCode = Hash(key);

            // 중복키 허용 X
            if (key.Equals(_data[hashCode].Key))
                return false;

            _data[hashCode] = new KeyValuePair<TKey, TValue>(key, value);
            return true;
        }

        public bool Contains(TKey key) {
            int hashCode = Hash(key);
            KeyValuePair<TKey, TValue> searched = _data[hashCode];
            return searched.Key.Equals(key);
        }

        public bool Remove(TKey key) {
            int hashCode = Hash(key);

            if (key.Equals(_data[hashCode].Key)) {
                _data[hashCode] = default;
                return true;
            }

            return false;
        }

        /// <summary>
        /// 문자열로 변환후 각 문자의 아스키값을 더한 후 크기로 나머지하는 간단한 해시함수
        /// </summary>
        int Hash(TKey key) {
            string str = key.ToString();
            int sum = 0;

            for (int i = 0; i < str.Length; i++) {
                sum += str[i];
            }

            sum %= DEFAULT_SIZE;
            return sum;
        }
    }
}