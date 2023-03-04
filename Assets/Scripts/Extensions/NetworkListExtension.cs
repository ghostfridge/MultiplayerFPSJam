using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;

public static class NetworkListExtension {
    public static T[] ToArray<T>(this NetworkList<T> networkList) where T : unmanaged, IEquatable<T> {
        T[] array = new T[networkList.Count];
        for (int i = 0; i < networkList.Count; i++) {
            array[i] = networkList[i];
        }
        return array;
    }

    public static List<T> ToList<T>(this NetworkList<T> networkList) where T : unmanaged, IEquatable<T> {
        List<T> list = new List<T>(networkList.Count);
        for (int i = 0; i < networkList.Count; i++) {
            list.Add(networkList[i]);
        }
        return list;
    }
}
