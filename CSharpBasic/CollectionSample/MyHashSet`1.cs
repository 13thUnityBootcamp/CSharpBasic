namespace CollectionsSample {
    class MyHashSet<T> {
        const int DEFAULT_SIZE = 5;
        LinkedList<T>[] _data = new LinkedList<T>[DEFAULT_SIZE];

        public bool Add(T item) {
            int hashCode = Hash(item);
            
            // 한 번도 이 인덱스에 값이 들어온적이 없다면 체이닝용 자료구조 하나 새로 만듬
            if (_data[hashCode] == null || _data[hashCode].First == null) {
                _data[hashCode] = new LinkedList<T>();
                _data[hashCode].AddFirst(item);
                return true;
            }
            // LinkedList는 있는데 아이템이 없다면 아이템이 있었다가 지워진 것임
            else if (_data[hashCode].First == null) {
                _data[hashCode].AddFirst(item);
                return true;
            }
            // 해시 충돌
            else {
                LinkedListNode<T> serachedNode = _data[hashCode].Find(item);

                // 중복값 허용 X
                if (serachedNode != null) {
                    return false;
                }
                else {
                    _data[hashCode].AddLast(item);
                    return true;
                }
            }
        }

        public bool Contains(T item) {
            // 아이템 탐색시
            // 해시코드 뽑고
            // 그걸로 인덱스 계산한다음
            // 인덱스 접근한 위치에서
            // 처음 노트부터 끝까지 다 탐색해서 원하는 아이템이 있는지 보고
            // 있으면 ture 없으면 false
            int hashCode = Hash(item);

            if (_data[hashCode] == null) {
                return false;
            }
            else {
                LinkedListNode<T> serachedNode = _data[hashCode].Find(item);
                return serachedNode != null;
            }
        }

        public bool Remove(T item) {
            int hashCode = Hash(item);

            if (_data[hashCode] == null)
                return false;

            return _data[hashCode].Remove(item);
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