using System.Runtime.Serialization;

namespace Core.Constant;

public enum OrderByType
{
    [EnumMember]
    Asc = 1,

    [EnumMember]
    Desc = 2
}