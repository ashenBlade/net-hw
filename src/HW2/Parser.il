.assembly extern System.Runtime
{
    .ver 5:0:0:0
    .publickeytoken = (B0 3F 5F 7F 11 D5 0A 3A)
}
.assembly extern HW1
{
    .ver 1:0:0:0
}
.assembly extern System.Console
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
    .class public auto ansi abstract sealed Parser
    extends [System.Runtime]System.Object
    {
        .method public static hidebysig
            class HW2.Result`1<valuetype [HW1]HW1.ParsingErrors> TryParseArguments
        (string[] args,
        [out] int32& val1,
        [out] string& operation,
        [out] int32& val2) cil managed
        {
            .maxstack 8
            .locals([0] class HW2.Result`1<valuetype [HW1]HW1.ParsingErrors> result)
            ldarg.0
            ldc.i4.0
            ldelem.ref // push args array at index 0 - left operand
            ldarg.1 // push val1 on stack
            call bool [System.Runtime]System.Int32::TryParse(string, int32&)

            ldarg.2
            ldarg.0
            ldc.i4.1
            ldelem.ref
            stind.ref // Store args[1] at string& operation

            ldarg.0
            ldc.i4.2
            ldelem.ref // push args array at index 2 - right operand
            ldarg.3 // push val2 on stack
            call bool [System.Runtime]System.Int32::TryParse(string, int32&)

            and
            brfalse OPERANDS_INVALID

            // Now operands are valid -> parsing operation

            ldarg.0
            ldc.i4.1
            ldelem.ref // operation string
            call bool [HW2]HW2.Parser::IsOperationSupported(string)
            brfalse OPERATION_NOT_SUPPORTED

            newobj instance void HW2.SuccessParsingResult::.ctor()
            ret

OPERANDS_INVALID:
            ldarg.0
            ldc.i4.0
            ldelem.ref // args[0]
            ldarg.0
            ldc.i4.1
            ldelem.ref // args[1]
            ldarg.0
            ldc.i4.2
            ldelem.ref // args[2]
            ldstr " are not valid arguments!"

            call string [System.Runtime]System.String::Concat(string, string, string, string)
            call void [System.Console]System.Console::WriteLine(string)
            newobj instance void HW2.OperandsInvalidResult::.ctor()
            ret

OPERATION_NOT_SUPPORTED:
            ldarg.0
            ldc.i4.1
            ldelem.ref
            ldstr " operation is not supported!"
            call string [System.Runtime]System.String::Concat(string, string)
            call void [System.Console]System.Console::WriteLine(string)
            newobj instance void HW2.OperationNotSupportedResult::.ctor()
            ret
        }

        .method private static hidebysig bool IsOperationSupported(string operation)
        {
            .maxstack 2
            ldarg.0
            ldstr "+"
            call bool [System.Runtime]System.String::op_Equality(string, string)
            brtrue TRUE

                        ldarg.0
            ldstr "-"
            call bool [System.Runtime]System.String::op_Equality(string, string)
            brtrue TRUE

                        ldarg.0
            ldstr "/"
            call bool [System.Runtime]System.String::op_Equality(string, string)
            brtrue TRUE

                        ldarg.0
            ldstr "*"
            call bool [System.Runtime]System.String::op_Equality(string, string)
            brtrue TRUE

            ldc.i4.0
            ret

TRUE:       ldc.i4.1
            ret
        }

        // Parse result codes
        .field public static literal int32 Success = int32(0)
        .field public static literal int32 OperandsInvalid = int32(1)
        .field public static literal int32 OperationNotSupported = int32(2)

}
}