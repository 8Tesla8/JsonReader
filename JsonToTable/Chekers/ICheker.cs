using System;
namespace JsonToTable.Chekers
{
    public interface ICheker<T>
    {
        T Check(T data, T filter);
    }
}
