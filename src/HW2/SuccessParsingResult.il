.assembly extern System.Runtime
{
    .ver 4:0:0:0
}

.assembly extern HW1
{
    .ver 1:0:0:0
}

.assembly HW2
{
    .ver 1:0:0:0
}

.module HW2.dll

.namespace HW2
{
    .class public ansi auto SuccessParsingResult
    extends class HW2.Result`1<valuetype [HW1]HW1.ParsingErrors>
    {
        .method public hidebysig rtspecialname specialname void .ctor() cil managed
        {
            .maxstack 4
            ldarg.0 // this
            ldc.i4.1 // true
            ldsfld string [System.Runtime]System.String::Empty // ""
            ldc.i4.0 // ParsingErrors.None
            call instance void class HW2.Result`1<valuetype [HW1]HW1.ParsingErrors>::.ctor(bool, string, !0)
            ret
        }
    }
}