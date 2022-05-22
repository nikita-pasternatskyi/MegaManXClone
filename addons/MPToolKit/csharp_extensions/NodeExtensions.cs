using Godot;
using System.Collections.Generic;
using System.Linq;

namespace MP.Extensions
{
    public static class NodeExtensions
    {
        public static void Disable(this Node me)
        {
            me.SetProcess(false);
            me.SetPhysicsProcess(false);
        }

        public static void Enable(this Node me)
        {
            me.SetProcess(true);
            me.SetPhysicsProcess(true);
        }

        public static List<T> GetChildren<T>(this Node me, bool recursive = false) where T : class
        {
            var array = me.GetChildren();
            List<T> result = new List<T>();
            for (int i = 0; i < array.Count; i++)
            {
                if (array[i] is T)
                    result.Add((T)array[i]);
            }
            if (recursive == true)
            {
                for (int i = 0; i < array.Count; i++)
                {
                    var node = array[i] as Node;
                    var newArr = node.GetChildren();
                    for (int j = 0; j < newArr.Count; j++)
                    {
                        object element = newArr[j];
                        if (element is T)
                            result.Add((T)element);
                        var children = ((Node)element).GetChildren<T>(true);
                        result.AddRange(children);
                    }
                }
            }

            return result;
        }

        public static bool TryGetNodeOfType<T>(this Node me, out T result, bool recursive = false, bool includeMe = false) where T:Node
        {
            result = null; 
            if(includeMe == true)
            {
                if (me is T)
                {
                    result = me as T;
                    return true;
                }
            }

            var results = me.GetChildren<T>(recursive);
            
            if (results.Count > 1)
            {
                GD.PrintErr($"there are two nodes of type {typeof(T)}!");
                return false;
            }

            if (results.IsEmpty() == false)
            {
                result = results[0];
                return true;
            }
            
            return false;
        }

        public static bool TryGetNodeInMeAndParent<T>(this Node me, out T result, bool parentInclusive = true, bool meInclusive = false, bool recursive = false) where T : Node
        {
            result = null;

            if(meInclusive == true)
            {
                if (me is T)
                {
                    result = me as T;
                    return true;
                }
            }

            var results = me.GetChildren<T>();
            if (results.Count > 1)
            {
                GD.PrintErr($"there are two nodes of type {typeof(T)}!");
                return false;
            }

            if (results.IsEmpty() == false)
            {
                result = results[0];
                return true;
            }

            GD.Print($"there are no nodes of type {typeof(T)} in children!");
            
            if (parentInclusive == true)
            {
                if (me.GetParent() is T)
                {
                    result = me.GetParent() as T;
                    return true;
                }
            }

            if (recursive == true)
            {
                results.Clear();
                var currentParent = me.GetParent();
                while (currentParent != null)
                {
                    results.AddRange(currentParent.GetChildren<T>());
                    currentParent = currentParent.GetParent();
                }
            }

            else
            {
                results = me.GetParent().GetChildren<T>();
            }

            if (results.Count > 1)
            {
                GD.PrintErr($"there are two nodes in parent {me.GetParent().Name} of type {typeof(T)}!");
                return false;
            }

            if (results.IsEmpty())
            {
                GD.PrintErr($"there are no nodes in parent {me.GetParent().Name} of type {typeof(T)}!");
                return false;
            }

            result = results[0];
            return true;
        }


        public static bool TryGetNodeFromPath<T>(this Node me, NodePath nodePath, out T result) where T : class
        {
            result = null;
            if (nodePath == null || nodePath.IsEmpty() == true)
            {
                GD.PrintErr($"{me.Name} to {typeof(T)} nodePath is empty!");
                return false;
            }

            var node = me.GetNodeOrNull(nodePath);

            if (node == null)
            {
                GD.PrintErr($"{me.Name} does not have a node under {nodePath}");
                return false;
            }

            if (typeof(T).IsAssignableFrom(node.GetType()) == false)
            {
                GD.PrintErr($"Cannot convert from type {typeof(T)} to {node.GetType()}");
                return false;
            }

            result = node as T;
            return true;
        }
    }
}