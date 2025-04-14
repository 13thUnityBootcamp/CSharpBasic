using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionsSample {
    // where 제한자 : 제네릭형식의 타입을 제한하는 키워드 (제네릭 형식의 타입은 제한된 타입으로 형변환이 가능해야함)
    class Node<T> where T : IComparable<T> {
        public T Value;
        public int Height; // Leaf에서 부터의 높이
        public Node<T> Left;
        public Node<T> Right;

        public void RefreshHeight() {
            //Height = Math.Max(Left?.Height ?? 0, Right?.Height ?? 0) + 1; 

            int leftHeight = 0;

            if(Left != null)
                leftHeight = Left.Height;

            int rightHeight = 0;

            if(Right != null)
                rightHeight = Right.Height;

            Height = Math.Max(leftHeight, rightHeight) + 1;
        }
    }

    class AVTTree<T> where T : IComparable<T> {
        Node<T> _root;

        /// <summary>
        /// 값의 존재여부
        /// </summary>
        /// <param name="value"> 찾으려는 값 </param>
        /// <returns> 존재 여부 </returns>
        public bool Contains(T value) {
            Node<T> current = _root;

            while (current != null) {
                int compare = value.CompareTo(current.Value);

                // 찾음
                if(compare ==0)
                    return true;
                // 찾으려는 값이 더 작으면 왼쪽으로 이동
                else if(compare < 0)
                    current = current.Left;
                // 찾으려는 값이 더 크면 오른쪽으로 이동
                else
                    current = current.Right;
            }
            return false; // 못 찾음
        }

        public void Add(T value) {
            _root = Add(_root, value);
        }
        
        /// <summary>
        /// node에 새 노드 추가 후 밸런싱 작업진행
        /// 밸런싱 이후 갱신된 부모 노드 반환
        /// </summary>
        /// <param name="node"> 값을 추가하려는 자리의 노드</param>
        /// <param name="value"> 추가하려는 값 </param>
        /// <returns></returns>
        private Node<T> Add(Node<T> node, T value) {
            // 1. node가 빈자리인지 확인 후 빈자리라면 새 노드 생성하여 value에 할당하고 반환
            // 2. node가 빈자리가 아니면 node의 값과 value를 비교하여
            //    value가 작으면 node 왼쪽 자식이 비었는지 1번 수행, 크면 node 오른쪽 자식이 비었는지 1번 수행
            // 3. node의 Height 계산 후 밸런싱이 필요하면 수행
            // 4. 밸런싱이 된 이후 현재까지의 최상위 노드 반환

            if(node == null)
                return new Node<T> { Value = value };

            int compare = value.CompareTo(node.Value);

            if (compare < 0)
                node.Left = Add(node.Left, value);
            else if (compare > 0)
                node.Right = Add(node.Right, value);
            else
                throw new Exception("이 이진트리는 중복값을 허용하지 않습니다.");

            node.RefreshHeight();
            int balance = CalcBalance(node);

            // 왼쪽 무너짐
            if (balance < -1) {
                // L-R
                if (value.CompareTo(node.Left.Value) > 0) {
                    node.Left = RotateLeft(node.Left);
                }

                // L-L
                return RotateRight(node);
            }
            // 오른쪽 무너짐
            else if (balance > 1) {
                // R-L
                if(value.CompareTo(node.Right.Value) < 0) {
                    node.Right = RotateRight(node.Right);
                }

                // R-R
                return RotateLeft(node);
            }
            // 밸런스 맞음
            else {
                return node;
            }
        }

        public void Remove(T value) {
            _root = Remove(_root, value);
        }

        private Node<T> Remove(Node<T> node, T value) {
            // 1. node가 빈자리면 값이 할당된 적 없는 위치므로 그냥 반환
            // 2. node가 빈자리가 아니면
            //    value와 node 값 비교해서 value가 더 작으면 왼쪽, 더 크면 오른쪽 자식에 대해 1번부터 수행
            //    value가 같으면 지우려는 노드를 찾은것임
            // 3. 지우려는 노드를 찾았다면,
            //    왼쪽 자식만 있다면 왼쪽 자식을 리턴하여 상위노드로 치환
            //    오른쪽 자식만 있다면 오른쪽 자식을 리턴하여 상위노드로 치환
            //    둘 다 있다면 오른쪽 자식의 왼쪽 DFS 결과 자식을 리턴하여 상위노드로 치환
            // 4. 밸런스 확인하고 리밸런싱후 반환

            if (node == null)
                return null;

            int compare = value.CompareTo(node.Value);

            // 지우려는 값이 왼쪽에 있을 것 같을때
            if (compare < 0) {
                node.Left = Remove(node.Left, value); // 왼쪽 자식에서 value 지우고 밸런싱 후 왼쪽 자식 갱신
            }
            // 지우려는 값이 오른쪽에 있을 것 같을때
            else if (compare > 0) {
                node.Right = Remove(node.Right, value);
            }
            // 지우려는 값 찾음
            else {
                // 왼쪽 없으면 오른쪽으로 상위로 치환
                if (node.Left == null) {
                    return node.Right;
                }
                // 오른쪽 없으면 왼쪽을 상위로 치환
                else if (node.Right == null) {
                    return node.Left;
                }
                // 둘다 있으면 오른쪽 자식의 왼쪽 DFS Leaf 노드를 상위로 치환
                else {
                    Node<T> tmp = node.Right;
                    Node<T> tmpParent = null;

                    while (tmp.Left != null) {
                        tmpParent = tmp;
                        tmp = tmp.Left;
                    }

                    node.Value = tmp.Value;
                    node.Right = Remove(node.Right, tmp.Value);
                }
            }

            node.RefreshHeight();
            int balance = CalcBalance(node);
            
            // 왼쪽 무너짐
            if(balance < -1) {
                // L-R
                if(value.CompareTo(node.Left.Value) > 0) {
                    node.Left = RotateLeft(node.Left);
                }

                // L=L
                return RotateLeft(node);
            }

            // 오른쪽 무너짐
            else if(balance > 1) {
                // R-L
                if(value.CompareTo(node.Right.Value) < 0) {
                    node.Right = RotateRight(node.Right);
                }

                // R-R
                return RotateRight(node);
            }
            // 밸런스 맞음
            else {
                return node; 
            }
        }

        /// <summary>
        /// 균형이 한쪽으로 무너졌는지 (왼쪽, 오른쪽이 높이 2 이상 차이나면 무너졌다고 판단할거임)
        /// </summary>
        /// <param name="node"> 균형 검사 대상</param>
        /// <returns> 균형 값 (-2보다 작으면 왼쪽, +2보다 크면 오른쪽으로 무너진 것) </returns>
        private int CalcBalance(Node<T> node) {
            int leftHeight = 0;

            if (node.Left != null)
                leftHeight = node.Left.Height;

            int rightHeight = 0;

            if (node.Right != null)
                rightHeight = node.Right.Height;

            return rightHeight - leftHeight;
        }

        /// <summary>
        /// 왼쪽으로 무너진 밸런스를 잡기 위해 오른쪽으로 노드를 회전시킴
        /// </summary>
        /// <param name="node"> 밸런스가 무너진 노드 </param>
        /// <returns> 밸런싱 이후 바뀐 부모 노드 </returns>
        private Node<T> RotateRight(Node<T> node) {
            // 밸런스가 왼쪽으로 무너진게 아니면 그냥 반환
            if (node == null || node.Left == null)
                return node;

            Node<T> newRoot = node.Left;
            node.Left = newRoot.Right;
            newRoot.Right = node;

            node.RefreshHeight();
            newRoot.RefreshHeight();
            return node;
        }

        /// <summary>
        /// 오른쪽으로 무너진 밸런스를 잡기 위해 왼쪽으로 노드를 회전시킴
        /// </summary>
        /// <param name="node"> 밸런스가 무너진 노드 </param>
        /// <returns> 밸런싱 이후 바뀐 부모 노드 </returns>
        private Node<T> RotateLeft(Node<T> node) {
            // 밸런스가 오른쪽으로 무너진게 아니면 그냥 반환
            if (node == null || node.Right == null)
                return node;

            Node<T> newRoot = node.Right;
            node.Right = newRoot.Left;
            newRoot.Left = node;

            node.RefreshHeight();
            newRoot.RefreshHeight();
            return node;
        }
    }
}
