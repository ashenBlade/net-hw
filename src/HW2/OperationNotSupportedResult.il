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
    .class public ansi auto OperationNotSupportedResult
    extends class HW2.Result`1<valuetype HW2.ParsingError>
    {
        .method public rtspecialname specialname void .ctor() cil managed
        {
            .maxstack 8
            ldarg.0 // this
            ldc.i4.0 // false
            ldstr "Operation not supported"
            ldc.i4.2 // ParsingError.None
            call instance void class HW2.Result`1<valuetype HW2.ParsingError>::.ctor(bool, string, !0)
            ret
        }
    }
}