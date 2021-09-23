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
    .class public ansi auto OperandsInvalidParsingResult
    extends class HW2.Result`1<valuetype HW2.ParsingError>
    {
        .method public hidebysig rtspecialname specialname instance void .ctor() cil managed
        {
            .maxstack 8
            ldarg.0 // this
            ldstr "Operands are invalid"
            call instance void class HW2.OperandsInvalidParsingResult::.ctor(string)
            ret
        }

        .method public rtspecialname specialname instance void .ctor
        (string message) cil managed
        {
            .maxstack 8
            ldarg.0 // this
            ldc.i4.0 // false
            ldarg.1 // message
            ldc.i4.1 // ParsingError.OperandsInvalid
            call instance void class HW2.Result`1<valuetype HW2.ParsingError>::.ctor(bool, string, !0)

            ret
        }
    }
}
