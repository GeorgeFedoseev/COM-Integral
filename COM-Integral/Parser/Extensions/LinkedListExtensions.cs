using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MathParser {
	internal static class LinkedListExtensions {
		public static int IndexOf<T>(this LinkedList<T> list, LinkedListNode<T> node) {
			if (node == null)
				return Int32.MaxValue;
			int index = 0;

			foreach (var x in list.GetNodes()) {
				if (x == node)
					return index;

				index++;
			}

			throw new InvalidOperationException("Provided node isn't contained in list.");
		}

		public static LinkedListNode<T> FindFirst<T>(this LinkedList<T> list, Predicate<T> match) {
			return FindFirst(list, list.First, match);
		}

		public static LinkedListNode<T> FindFirst<T>(this LinkedList<T> list, LinkedListNode<T> startNode, Predicate<T> match) {
			foreach (var node in startNode.GetNodes()) {
				var value = node.Value;
				if (match(value))
					return node;
			}

			return null;
		}

		public static IEnumerable<LinkedListNode<T>> GetNodes<T>(this LinkedListNode<T> node) {
			do {
				yield return node;
				node = node.Next;
			}
			while (node != null);
		}

		public static LinkedListNode<T> FindLast<T>(this LinkedList<T> list, Predicate<T> match) {
			foreach (var node in list.GetNodesReversed()) {
				var value = node.Value;
				if (match(value))
					return node;
			}

			return null;
		}

		public static LinkedListNode<T> FindLastNode<T>(this LinkedList<T> list, Predicate<LinkedListNode<T>> match) {
			foreach (var node in list.GetNodesReversed()) {
				if (match(node))
					return node;
			}

			return null;
		}

		public static IEnumerable<LinkedListNode<T>> GetNodes<T>(this LinkedList<T> list) {
			return list.First.GetNodes();
		}

		public static IEnumerable<LinkedListNode<T>> GetNodesReversed<T>(this LinkedList<T> list) {
			var node = list.Last;
			do {
				yield return node;
				node = node.Previous;
			}
			while (node != null);
		}

		public static void Remove<T>(this LinkedListNode<T> node) {
			node.List.Remove(node);
		}

		public static void RemoveSubList<T>(this LinkedListNode<T> start, LinkedListNode<T> end) {
			var list = start.List;
			var i = start;
			do {
				var next = i.Next;
				list.Remove(i);
				if (i == end)
					break;
				i = next;
			} while (i != null && i.Previous != end);
		}

		public static void RemovePrevious<T>(this LinkedListNode<T> node) {
			node.List.Remove(node.Previous);
		}

		public static void RemoveNext<T>(this LinkedListNode<T> node) {
			node.List.Remove(node.Next);
		}

		public static LinkedList<T> GetSubList<T>(this LinkedListNode<T> beforeStart, LinkedListNode<T> afterEnd) {
			LinkedList<T> result = new LinkedList<T>(GetSubListItems(beforeStart, afterEnd));
			return result;
		}

		private static IEnumerable<T> GetSubListItems<T>(LinkedListNode<T> beforeStart, LinkedListNode<T> afterEnd) {
			LinkedListNode<T> i = beforeStart.Next;
			while (i != afterEnd) {
				if (i == null)
					throw new Exception("Unexpected end of linked list.");

				yield return i.Value;
				i = i.Next;
			}
		}
	}
}
