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
    .class enum sealed  public ParsingError
        extends [System.Runtime]System.Enum
    {
        .field public static literal int32 Success = int32(0)
        .field public static literal int32 OperandsInvalid = int32(1)
        .field public static literal int32 OperationNotSupported = int32(2)
    }

}