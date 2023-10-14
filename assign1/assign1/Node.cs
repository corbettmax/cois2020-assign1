using System;

namespace assign1
{
    public class Node<T>
    {
        public T Item { get; set; }
        public Node<T> Next { get; set; }

        public Node(T item, Node<T> next)
        {
            Item = item;
            Next = next;
        }

    }
}
