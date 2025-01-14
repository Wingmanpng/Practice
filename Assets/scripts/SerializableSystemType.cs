// Simple helper class that allows you to serialize System.Type objects.
// Use it however you like, but crediting or even just contacting the author would be appreciated (Always 
// nice to see people using your stuff!)
//
// Written by Bryan Keiren (http://www.bryankeiren.com)

using UnityEngine;
using System.Runtime.Serialization;
using System;
using System.IO;

[System.Serializable]
public class SerializableSystemType : ISerializationCallbackReceiver
{
    public System.Type type;
    public byte[] data;
    public SerializableSystemType(System.Type aType)
    {
        type = aType;
    }

    public static System.Type Read(BinaryReader aReader)
    {
        var paramCount = aReader.ReadByte();
        if (paramCount == 0xFF)
            return null;
        var typeName = aReader.ReadString();
        var type = System.Type.GetType(typeName);
        if (type == null)
            throw new System.Exception("Can't find type; '" + typeName + "'");
        if (type.IsGenericTypeDefinition && paramCount > 0)
        {
            var p = new System.Type[paramCount];
            for (int i = 0; i < paramCount; i++)
            {
                p[i] = Read(aReader);
            }
            type = type.MakeGenericType(p);
        }
        return type;
    }

    public static void Write(BinaryWriter aWriter, System.Type aType)
    {
        if (aType == null)
        {
            aWriter.Write((byte)0xFF);
            return;
        }
        if (aType.IsGenericType)
        {
            var t = aType.GetGenericTypeDefinition();
            var p = aType.GetGenericArguments();
            aWriter.Write((byte)p.Length);
            aWriter.Write(t.AssemblyQualifiedName);
            for (int i = 0; i < p.Length; i++)
            {
                Write(aWriter, p[i]);
            }
            return;
        }
        aWriter.Write((byte)0);
        aWriter.Write(aType.AssemblyQualifiedName);
    }


    public void OnBeforeSerialize()
    {
        using (var stream = new MemoryStream())
        using (var w = new BinaryWriter(stream))
        {
            Write(w, type);
            data = stream.ToArray();
        }
    }

    public void OnAfterDeserialize()
    {
        using (var stream = new MemoryStream(data))
        using (var r = new BinaryReader(stream))
        {
            type = Read(r);
        }
    }
}