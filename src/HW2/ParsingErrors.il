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
    .class sealed enum public ParsingErrors
    {
        .field public static literal valuetype HW2.ParsingErrors None = int32(0)
        .field public static literal valuetype HW2.ParsingErrors OperandsInvalid = int32(1)
        .field public static literal valuetype HW2.ParsingErrors OperationNotSupported = int32(2)
    }

}