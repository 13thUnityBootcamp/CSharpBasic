using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Trie 작성 스펙

//1. Root 있어야함
//2. 문자열을 추가하는 함수
//문자열 추가시, 알파벳 하나씩 누적시킨 트리를 자식으로계속 추가하여 
//단어전체를 가지는 리프노드를 만들때까지 자식 계속추가

//3. 탐색 함수
//1) 특정 단어를 찾아서 존재여부를 반환하는 함수
//2) 인자로 받은문자열로 시작하는 모든 단어 (Leaf) 목록을 반환하는 함수

//4. 삭제 기능
//1) 특정 단어 삭제 기능
//2) 전체삭제기능


//* 입력 제한 
//입력은 알파벳만 가능하다 (a ~ z , A ~ Z) 
//( 대문자를 소문자로 바꾸는 함수는 string.ToLower() )
//자동완성은 소문자로만 저장한다.


//* 힌트 
//문자열을 순회하려면 
//for 루프로 index 접근하던지 / foreach 문으로 열거하던지

namespace CollectionsSample {
    class TrieNode {
        public TrieNode this[char alphabet] {
            get {
                return _children[alphabet - 'a'];
            }
            set {
                if (value == null) {
                    _count--;
                }
                else {
                    _count++;
                }

                _children[alphabet - 'a'] = value;
            }
        }

        public bool HasChild => _count == 0;

        const int ALPHABET_TOTAL = 26;

        public bool IsWord;
        private int _count;
        TrieNode[] _children = new TrieNode[ALPHABET_TOTAL];
    }

    class Trie {
        const int ALPHABET_TOTAL = 26;
        TrieNode _root = new TrieNode();

        public void Add(string word) {
            TrieNode current = _root;

            // 들어온 단어 전체 순회
            foreach (char alphabet in word) {
                char c = char.ToLower(alphabet); // 소문자로

                // 현재까지의 Prefix 가 저장된적 있는지 확인. 저장된적 없으면 새 노드 생성
                if (current[c] == null) {
                    current[c] = new TrieNode();
                }

                current = current[c]; // 다음 자식으로 이동
            }

            current.IsWord = true; // 단어전체 저장 완료되었으므로 현재 노드는 완성된 단어를 의미하는 노드.
        }

        public bool Contains(string word) {
            TrieNode current = _root;

            // 들어온 단어 전체 순회
            foreach (char alphabet in word) {
                char c = char.ToLower(alphabet); // 소문자로

                // 현재까지의 Prefix 가 저장된적 있는지 확인. 저장된적 없으면 이 단어는 저장된적 없음
                if (current[c] == null) {
                    return false;
                }

                current = current[c]; // 다음 자식으로 이동
            }

            return current.IsWord;
        }

        public List<string> StartsWith(string prefix) {
            List<string> results = new List<string>();
            TrieNode current = _root;

            // Prefix 노드로 이동
            foreach (char alphabet in prefix) {
                char c = char.ToLower(alphabet); // 소문자로

                // prefix 가 존재하지 않을 때
                if (current[c] == null) {
                    return results;
                }

                current = current[c]; // 다음 자식으로 이동
            }

            FindAllWords(current, prefix, results);
            return results;
        }

        private void FindAllWords(TrieNode node, string buildingWord, List<string> appendList) {
            if (node.IsWord)
                appendList.Add(buildingWord);

            for (char c = 'a'; c <= 'z'; c++) {
                if (node[c] == null)
                    continue;

                FindAllWords(node[c], buildingWord + c, appendList);
            }
        }

        public bool Remove(string word) {
            // 1. 삭제할 단어의 경로를 기억
            // 2. 지우려는 단어 경로 추적
            // 3. 지우려는 단어 노드 의 IsWord 를 false 로한다.
            // 4. 지우려는 단어 노드가 자식을 가진다면 지울 수 있는 노드가 없으므로 종료한다.
            // 5. 지우려는 단어 노드가 자식을 가지지 않는다면 경로를 역추적하면서 
            // 삭제 경로외에 자식을가지지 않는 노드를 모두 삭제한다.

            List<(TrieNode parent, char alphabet)> path = new List<(TrieNode parent, char alphabet)>();

            TrieNode current = _root;

            foreach (char alphabet in word) {
                char c = char.ToLower(alphabet);

                // 이 단어는 등록된적이 없다면 반환
                if (current[c] == null) {
                    return false;
                }

                path.Add((current, c));
                current = current[c];
            }

            // 지우려는걸 찾았지만 다른 문자열의 일부일뿐, 단어가 등록된적이 없으면 반환
            if (current.IsWord == false)
                return false;

            current.IsWord = false;

            // 지웠지만 지운거까지의 노드가 다른 문자열의 일부로 사용되고있다면 노드자체는 놔둬야함
            if (current.HasChild)
                return true;

            // 역추적하여 지울수있는 상위노드를 모두 삭제
            for (int i = path.Count - 1; i >= 0; i--) {
                (TrieNode parent, char alphabet) pair = path[i];
                pair.parent[pair.alphabet] = null;

                // 상위노드가 자식을 가지므로 더이상 지우면 안됨
                if (pair.parent.HasChild)
                    break;

                // 현재 노드가 독립적인 단어라면 더이상 지우면안됨
                if (pair.parent.IsWord)
                    break;
            }

            return true;
        }

        public void Clear() {
            _root = new TrieNode();
        }
    }
}