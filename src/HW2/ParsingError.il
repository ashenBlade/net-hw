.assembly extern System.Runtime
{
    .ver 4:0:0:0
}

.assembly HW2
{
    .ver 1:0:0:0
}

.module HW2.dll

.namespace HW2
{
    .class sealed ansi auto public ParsingError
        extends [System.Runtime]System.Enum
    {
        .field public static literal valuetype HW2.ParsingError None = int32(0)
        .field public static literal valuetype HW2.ParsingError OperandsInvalid = int32(1)
        .field public static literal valuetype HW2.ParsingError OperationNotSupported = int32(2)
    }

}