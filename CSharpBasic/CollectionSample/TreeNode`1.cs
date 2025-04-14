using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionsSample
{
    class TreeNode<T>
    {
        public TreeNode(T value, int capacity = 2) {
            Value = value;  
            Children = new List<TreeNode<T>>(capacity);
        }

        public T Value;
        public List<TreeNode<T>> Children;

        class Tree<T> {
            /// <summary>
            /// 최상단 노드
            /// </summary>
            public TreeNode<T> Root { get; private set; }

            public void AddChild(TreeNode<T> parent, T childValue) {
                parent.Children.Add(new TreeNode<T>(childValue));
            }

            public void RemoveChild(TreeNode<T> parent, T childValue) {
                int index = parent.Children.FindIndex(x => x.Value.Equals(childValue));

                if (index < 0)
                    throw new ArgumentException($"{childValue} 라는 값을 가진 자식이 없습니다");

                parent.Children.RemoveAt(index);
            }

            public TreeNode<T> Find(T value) {
                return Find(Root, value);
            }

            private TreeNode<T> Find(TreeNode<T> node, T value) {
                // 탐색하는 노드가 null이면 null 반환
                if (node == null)
                    return null;

                // 탐색하려는 노드 찾음
                if (node.Value.Equals(value)) return node;

                // 찾지 못했으면 모든 자식에 대해 탐색 수행
                foreach (TreeNode<T> child in node.Children) {
                    TreeNode<T> foundValue = Find(child, value);

                    // 현재 자식이 찾는 노드라면 해당 자식 반환
                    if (foundValue.Equals(value))
                        return foundValue;
                }

                return null; // 현재 노드는 모든 자식들에게 찾는 값이 존재하지 않음
            }
        }
    }
}
