using System;
using System.Collections.Generic;

namespace JsonToTable.Desirializers
{
    public interface IDeserializerItem<T>
    {
        T DeserializeItem(string data);
    }


    public interface IDeserializerArray<T> 
    {   
        List<T> DeserializeArray(string data);
    }


    public interface IDeserializer<T> : IDeserializerItem<T>, IDeserializerArray<T>
    {
        //List<T> DeserializeArray(string data);
        //T DeserializeItem(string data);

        dynamic DeserializeDynamic(string data);
    }
}
