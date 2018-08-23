using System;   
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using UMLer.Paintables;

namespace UMLer.Special
{
    public class ControledHashSet : ICollection<IPaintable>, IEnumerable<IPaintable>, IEnumerable, ISerializable, IDeserializationCallback, ISet<IPaintable>, IReadOnlyCollection<IPaintable>
    {
        private HashSet<IPaintable> hashSet;

        public Diagram Diagram { get; set; }

        //
        // Summary:
        //     Initializes a new instance of the System.Collections.Generic.HashSet`1 class
        //     that is empty and uses the default equality comparer for the set type.
        public ControledHashSet()
        {
            hashSet = new HashSet<IPaintable>();
        }
        //
        // Summary:
        //     Initializes a new instance of the System.Collections.Generic.HashSet`1 class
        //     that is empty and uses the specified equality comparer for the set type.
        //
        // Parameters:
        //   comparer:
        //     The System.Collections.Generic.IEqualityComparer`1 implementation to use when
        //     comparing values in the set, or null to use the default System.Collections.Generic.EqualityComparer`1
        //     implementation for the set type.
        public ControledHashSet(IEqualityComparer<IPaintable> comparer)
        {
            
            hashSet = new HashSet<IPaintable>(comparer);
        }
        //
        // Summary:
        //     Initializes a new instance of the System.Collections.Generic.HashSet`1 class
        //     that uses the default equality comparer for the set type, contains elements copied
        //     from the specified collection, and has sufficient capacity to accommodate the
        //     number of elements copied.
        //
        // Parameters:
        //   collection:
        //     The collection whose elements are copied to the new set.
        //
        // Exceptions:
        //   IPaintable:System.ArgumentNullException:
        //     collection is null.
        public ControledHashSet(IEnumerable<IPaintable> collection)
        {
            hashSet = new HashSet<IPaintable>(collection);
        }
        //
        // Summary:
        //     Initializes a new instance of the System.Collections.Generic.HashSet`1 class
        //     that uses the specified equality comparer for the set type, contains elements
        //     copied from the specified collection, and has sufficient capacity to accommodate
        //     the number of elements copied.
        //
        // Parameters:
        //   collection:
        //     The collection whose elements are copied to the new set.
        //
        //   comparer:
        //     The System.Collections.Generic.IEqualityComparer`1 implementation to use when
        //     comparing values in the set, or null to use the default System.Collections.Generic.EqualityComparer`1
        //     implementation for the set type.
        //
        // Exceptions:
        //   IPaintable:System.ArgumentNullException:
        //     collection is null.
        public ControledHashSet(IEnumerable<IPaintable> collection, IEqualityComparer<IPaintable> comparer)
        {
            hashSet = new HashSet<IPaintable>(collection,comparer);
        }
        //
        // Summary:
        //     Initializes a new instance of the System.Collections.Generic.HashSet`1 class
        //     with serialized data.
        //
        // Parameters:
        //   info:
        //     A System.Runtime.Serialization.SerializationInfo object that contains the information
        //     required to serialize the System.Collections.Generic.HashSet`1 object.
        //
        //   context:
        //     A System.Runtime.Serialization.StreamingContext structure that contains the source
        //     and destination of the serialized stream associated with the System.Collections.Generic.HashSet`1
        //     object.

        //
        // Summary:
        //     Gets the number of elements that are contained in a set.
        //
        // Returns:
        //     The number of elements that are contained in the set.
        public int Count { get => hashSet.Count; }
        //
        // Summary:
        //     Gets the System.Collections.Generic.IEqualityComparer`1 object that is used to
        //     determine equality for the values in the set.
        //
        // Returns:
        //     The System.Collections.Generic.IEqualityComparer`1 object that is used to determine
        //     equality for the values in the set.
        public IEqualityComparer<IPaintable> Comparer { get => hashSet.Comparer; }

        public bool IsReadOnly => false;

        //
        // Summary:
        //     Adds the specified element to a set.
        //
        // Parameters:
        //   item:
        //     The element to add to the set.
        //
        // Returns:
        //     true if the element is added to the System.Collections.Generic.HashSet`1 object;
        //     false if the element is already present.
        public bool Add(IPaintable item)
        {
            return hashSet.Add(item);
        }
        //
        // Summary:
        //     Removes all elements from a System.Collections.Generic.HashSet`1 object.
        public void Clear()
        {
            lock (hashSet)
            {
                foreach (var item in hashSet.ToList())
                {
                    if(item != null)
                        item.OnDeleted();
                }
            }
            hashSet.Clear();
            Diagram.ElementPanel.Refresh();
        }
        //
        // Summary:
        //     Determines whether a System.Collections.Generic.HashSet`1 object contains the
        //     specified element.
        //
        // Parameters:
        //   item:
        //     The element to locate in the System.Collections.Generic.HashSet`1 object.
        //
        // Returns:
        //     true if the System.Collections.Generic.HashSet`1 object contains the specified
        //     element; otherwise, false.
        public bool Contains(IPaintable item)
        {
            return hashSet.Contains(item);
        }
        //
        // Summary:
        //     Copies the elements of a System.Collections.Generic.HashSet`1 object to an array,
        //     starting at the specified array index.
        //
        // Parameters:
        //   array:
        //     The one-dimensional array that is the destination of the elements copied from
        //     the System.Collections.Generic.HashSet`1 object. The array must have zero-based
        //     indexing.
        //
        //   arrayIndex:
        //     The zero-based index in array at which copying begins.
        //
        // Exceptions:
        //   IPaintable:System.ArgumentNullException:
        //     array is null.
        //
        //   IPaintable:System.ArgumentOutOfRangeException:
        //     arrayIndex is less than 0.
        //
        //   IPaintable:System.ArgumentException:
        //     arrayIndex is greater than the length of the destination array.
        public void CopyTo(IPaintable[] array, int arrayIndex)
        {
            hashSet.CopyTo(array, arrayIndex);
        }
        //
        // Summary:
        //     Copies the specified number of elements of a System.Collections.Generic.HashSet`1
        //     object to an array, starting at the specified array index.
        //
        // Parameters:
        //   array:
        //     The one-dimensional array that is the destination of the elements copied from
        //     the System.Collections.Generic.HashSet`1 object. The array must have zero-based
        //     indexing.
        //
        //   arrayIndex:
        //     The zero-based index in array at which copying begins.
        //
        //   count:
        //     The number of elements to copy to array.
        //
        // Exceptions:
        //   IPaintable:System.ArgumentNullException:
        //     array is null.
        //
        //   IPaintable:System.ArgumentOutOfRangeException:
        //     arrayIndex is less than 0.-or-count is less than 0.
        //
        //   IPaintable:System.ArgumentException:
        //     arrayIndex is greater than the length of the destination array.-or-count is greater
        //     than the available space from the index to the end of the destination array.
        public void CopyTo(IPaintable[] array, int arrayIndex, int count)
        {
            hashSet.CopyTo(array, arrayIndex, count);
        }
        //
        // Summary:
        //     Copies the elements of a System.Collections.Generic.HashSet`1 object to an array.
        //
        // Parameters:
        //   array:
        //     The one-dimensional array that is the destination of the elements copied from
        //     the System.Collections.Generic.HashSet`1 object. The array must have zero-based
        //     indexing.
        //
        // Exceptions:
        //   IPaintable:System.ArgumentNullException:
        //     array is null.
        public void CopyTo(IPaintable[] array)
        {
            hashSet.CopyTo(array);
        }
        //
        // Summary:
        //     Removes all elements in the specified collection from the current System.Collections.Generic.HashSet`1
        //     object.
        //
        // Parameters:
        //   other:
        //     The collection of items to remove from the System.Collections.Generic.HashSet`1
        //     object.
        //
        // Exceptions:
        //   IPaintable:System.ArgumentNullException:
        //     other is null.
        public void ExceptWith(IEnumerable<IPaintable> other)
        {
            hashSet.ExceptWith(other);
        }
        //
        // Summary:
        //     Returns an enumerator that iterates through a System.Collections.Generic.HashSet`1
        //     object.
        //
        // Returns:
        //     A System.Collections.Generic.HashSet`1.Enumerator object for the System.Collections.Generic.HashSet`1
        //     object.
        public HashSet<IPaintable>.Enumerator GetEnumerator()
        {
            return hashSet.GetEnumerator();
        }
        //
        // Summary:
        //     Implements the System.Runtime.Serialization.ISerializable interface and returns
        //     the data needed to serialize a System.Collections.Generic.HashSet`1 object.
        //
        // Parameters:
        //   info:
        //     A System.Runtime.Serialization.SerializationInfo object that contains the information
        //     required to serialize the System.Collections.Generic.HashSet`1 object.
        //
        //   context:
        //     A System.Runtime.Serialization.StreamingContext structure that contains the source
        //     and destination of the serialized stream associated with the System.Collections.Generic.HashSet`1
        //     object.
        //
        // Exceptions:
        //   IPaintable:System.ArgumentNullException:
        //     info is null.
        [SecurityCritical]
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            hashSet.GetObjectData(info, context);
        }
        //
        // Summary:
        //     Modifies the current System.Collections.Generic.HashSet`1 object to contain only
        //     elements that are present in that object and in the specified collection.
        //
        // Parameters:
        //   other:
        //     The collection to compare to the current System.Collections.Generic.HashSet`1
        //     object.
        //
        // Exceptions:
        //   IPaintable:System.ArgumentNullException:
        //     other is null.
        public void IntersectWith(IEnumerable<IPaintable> other)
        {
            hashSet.IntersectWith(other);
        }
        //
        // Summary:
        //     Determines whether a System.Collections.Generic.HashSet`1 object is a proper
        //     subset of the specified collection.
        //
        // Parameters:
        //   other:
        //     The collection to compare to the current System.Collections.Generic.HashSet`1
        //     object.
        //
        // Returns:
        //     true if the System.Collections.Generic.HashSet`1 object is a proper subset of
        //     other; otherwise, false.
        //
        // Exceptions:
        //   IPaintable:System.ArgumentNullException:
        //     other is null.
        public bool IsProperSubsetOf(IEnumerable<IPaintable> other)
        {
            return hashSet.IsProperSubsetOf(other);
        }
        //
        // Summary:
        //     Determines whether a System.Collections.Generic.HashSet`1 object is a proper
        //     superset of the specified collection.
        //
        // Parameters:
        //   other:
        //     The collection to compare to the current System.Collections.Generic.HashSet`1
        //     object.
        //
        // Returns:
        //     true if the System.Collections.Generic.HashSet`1 object is a proper superset
        //     of other; otherwise, false.
        //
        // Exceptions:
        //   IPaintable:System.ArgumentNullException:
        //     other is null.
        public bool IsProperSupersetOf(IEnumerable<IPaintable> other)
        {
            return hashSet.IsProperSupersetOf(other);
        }
        //
        // Summary:
        //     Determines whether a System.Collections.Generic.HashSet`1 object is a subset
        //     of the specified collection.
        //
        // Parameters:
        //   other:
        //     The collection to compare to the current System.Collections.Generic.HashSet`1
        //     object.
        //
        // Returns:
        //     true if the System.Collections.Generic.HashSet`1 object is a subset of other;
        //     otherwise, false.
        //
        // Exceptions:
        //   IPaintable:System.ArgumentNullException:
        //     other is null.
        public bool IsSubsetOf(IEnumerable<IPaintable> other)
        {
            return hashSet.IsSubsetOf(other);
        }
        //
        // Summary:
        //     Determines whether a System.Collections.Generic.HashSet`1 object is a superset
        //     of the specified collection.
        //
        // Parameters:
        //   other:
        //     The collection to compare to the current System.Collections.Generic.HashSet`1
        //     object.
        //
        // Returns:
        //     true if the System.Collections.Generic.HashSet`1 object is a superset of other;
        //     otherwise, false.
        //
        // Exceptions:
        //   IPaintable:System.ArgumentNullException:
        //     other is null.
        public bool IsSupersetOf(IEnumerable<IPaintable> other)
        {
            return hashSet.IsSubsetOf(other);
        }
        //
        // Summary:
        //     Implements the System.Runtime.Serialization.ISerializable interface and raises
        //     the deserialization event when the deserialization is complete.
        //
        // Parameters:
        //   sender:
        //     The source of the deserialization event.
        //
        // Exceptions:
        //   IPaintable:System.Runtime.Serialization.SerializationException:
        //     The System.Runtime.Serialization.SerializationInfo object associated with the
        //     current System.Collections.Generic.HashSet`1 object is invalid.
        public virtual void OnDeserialization(object sender)
        {
            hashSet.OnDeserialization(sender);
        }
        //
        // Summary:
        //     Determines whether the current System.Collections.Generic.HashSet`1 object and
        //     a specified collection share common elements.
        //
        // Parameters:
        //   other:
        //     The collection to compare to the current System.Collections.Generic.HashSet`1
        //     object.
        //
        // Returns:
        //     true if the System.Collections.Generic.HashSet`1 object and other share at least
        //     one common element; otherwise, false.
        //
        // Exceptions:
        //   IPaintable:System.ArgumentNullException:
        //     other is null.
        public bool Overlaps(IEnumerable<IPaintable> other)
        {
            return hashSet.Overlaps(other);
        }
        //
        // Summary:
        //     Removes the specified element from a System.Collections.Generic.HashSet`1 object.
        //
        // Parameters:
        //   item:
        //     The element to remove.
        //
        // Returns:
        //     true if the element is successfully found and removed; otherwise, false. This
        //     method returns false if item is not found in the System.Collections.Generic.HashSet`1
        //     object.
        public bool Remove(IPaintable item)
        {
            if (item != null && item is ISubordinate)
            {
                ((ISubordinate)item).DeleteRequested();
            }
            else
            {
                item?.OnDeleted();
            }
            if (item != null && item.IsFocused())
            {
                item.Parent.ForceFocusAround(null);
            }
            return hashSet.Remove(item);
        }

        /// <summary>
        /// Removes item without alerting subordinates
        /// or calling events
        /// </summary>
        /// <param name="item">item to delete</param>
        /// <returns>true if successful false otherwise</returns>
        public bool SilentRemove(IPaintable item)
        {
            if (item != null && item.IsFocused())
            {
                item.Parent.ForceFocusAround(null);
            }
            return hashSet.Remove(item);
        }

        /// <summary>
        /// Removes items matching predicate without 
        /// alerting subordinates or calling events
        /// </summary>
        /// <param name="match">predicate</param>
        /// <returns>amount of items removed</returns>
        public int SilentRemoveWhere(Predicate<IPaintable> match)
        {
            return hashSet.RemoveWhere(match);
        }


        //
        // Summary:
        //     Removes all elements that match the conditions defined by the specified predicate
        //     from a System.Collections.Generic.HashSet`1 collection.
        //
        // Parameters:
        //   match:
        //     The System.Predicate`1 delegate that defines the conditions of the elements to
        //     remove.
        //
        // Returns:
        //     The number of elements that were removed from the System.Collections.Generic.HashSet`1
        //     collection.
        //
        // Exceptions:
        //   IPaintable:System.ArgumentNullException:
        //     match is null.
        public int RemoveWhere(Predicate<IPaintable> match)
        {
            foreach (var item in hashSet)
            {

                if (match(item))
                {
                    if (item != null && item is ISubordinate)
                    {
                        ((ISubordinate)item).DeleteRequested();
                    }
                    else
                    {
                        item.OnDeleted();
                    }
                    if (item != null && item.IsFocused())
                    {
                        item.Parent.ForceFocusAround(null);
                    }
                }
                
            }
            return hashSet.RemoveWhere(match);
        }
        //
        // Summary:
        //     Determines whether a System.Collections.Generic.HashSet`1 object and the specified
        //     collection contain the same elements.
        //
        // Parameters:
        //   other:
        //     The collection to compare to the current System.Collections.Generic.HashSet`1
        //     object.
        //
        // Returns:
        //     true if the System.Collections.Generic.HashSet`1 object is equal to other; otherwise,
        //     false.
        //
        // Exceptions:
        //   IPaintable:System.ArgumentNullException:
        //     other is null.
        public bool SetEquals(IEnumerable<IPaintable> other)
        {
            return hashSet.SetEquals(other);
        }
        //
        // Summary:
        //     Modifies the current System.Collections.Generic.HashSet`1 object to contain only
        //     elements that are present either in that object or in the specified collection,
        //     but not both.
        //
        // Parameters:
        //   other:
        //     The collection to compare to the current System.Collections.Generic.HashSet`1
        //     object.
        //
        // Exceptions:
        //   IPaintable:System.ArgumentNullException:
        //     other is null.
        public void SymmetricExceptWith(IEnumerable<IPaintable> other)
        {
            hashSet.SymmetricExceptWith(other);
        }
        //
        // Summary:
        //     Sets the capacity of a System.Collections.Generic.HashSet`1 object to the actual
        //     number of elements it contains, rounded up to a nearby, implementation-specific
        //     value.
        public void TrimExcess()
        {
            hashSet.TrimExcess();
        }
        //
        // Summary:
        //     Modifies the current System.Collections.Generic.HashSet`1 object to contain all
        //     elements that are present in itself, the specified collection, or both.
        //
        // Parameters:
        //   other:
        //     The collection to compare to the current System.Collections.Generic.HashSet`1
        //     object.
        //
        // Exceptions:
        //   IPaintable:System.ArgumentNullException:
        //     other is null.
        public void UnionWith(IEnumerable<IPaintable> other)
        {
            hashSet.UnionWith(other);
        }

        void ICollection<IPaintable>.Add(IPaintable item)
        {
            hashSet.Add(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return hashSet.GetEnumerator();
        }

        IEnumerator<IPaintable> IEnumerable<IPaintable>.GetEnumerator()
        {
            return hashSet.GetEnumerator();
        }
    }
}
