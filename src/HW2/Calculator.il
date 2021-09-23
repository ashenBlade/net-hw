.assembly extern System.Runtime
{
    .ver 4:0:0:0
    .publickeytoken = (B0 3F 5F 7F 11 D5 0A 3A)
}

.assembly extern System.Concole
{
    .ver 4:0:0:0
}

.assembly HW2
{
    .ver 1:0:0:0
}

.module HW2.dll

.namespace HW2 {

.class public auto ansi abstract sealed beforefieldinit Calculator
    extends [System.Runtime]System.Object
{
    .method public hidebysig static
        int32 Calculate(int32 val1, string op, int32 val2) cil managed
    {
        .maxstack 8
        .locals([0] int32 eq)

        ldarg.1
        ldstr "+"
        call bool [System.Runtime]System.String::op_Equality(string, string)
        brtrue PLUS

        ldarg.1
        ldstr "-"
        call bool [System.Runtime]System.String::op_Equality(string, string)
        brtrue MINUS

        ldarg.1
        ldstr "/"
        call bool [System.Runtime]System.String::op_Equality(string, string)
        brtrue DIV

        ldarg.1
        ldstr "*"
        call bool [System.Runtime]System.String::op_Equality(string, string)
        brtrue MUL

        br DEF

  PLUS: ldarg.0
        ldarg.2
        add
        stloc.0
        br RET

  MINUS:ldarg.0
        ldarg.2
        sub
        stloc.0
        br RET

  DIV:  ldarg.0
        ldarg.2
        div
        stloc.0
        br RET

  MUL:  ldarg.0
        ldarg.2
        mul
        stloc.0
        br RET

  DEF:  ldc.i4.0
        stloc.0


  RET:  ldloc.0
        ret
  }
  }
}