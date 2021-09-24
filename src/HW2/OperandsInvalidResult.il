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
    .class public ansi auto OperandsInvalidResult
    extends class HW2.Result`1<valuetype [HW1]HW1.ParsingErrors>
    {
        .method public hidebysig rtspecialname specialname instance void .ctor() cil managed
        {
            .maxstack 3
            ldarg.0 // this
            ldstr "Operands are invalid"
            call instance void class HW2.OperandsInvalidResult::.ctor(string)
            ret
        }

        .method public rtspecialname specialname instance void .ctor
        (string message) cil managed
        {
            .maxstack 4
            ldarg.0 // this
            ldc.i4.0 // false
            ldarg.1 // message
            ldc.i4.1 // ParsingErrors.OperandsInvalid
            call instance void class HW2.Result`1<valuetype [HW1]HW1.ParsingErrors>::.ctor(bool, string, !0)

            ret
        }
    }
}
