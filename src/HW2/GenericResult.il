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
    .class public ansi auto beforefieldinit Result`1<T>
    extends [HW2]HW2.Result
    {
        .field private !0 _value

        .method private instance void set_value(!0 'value') cil managed
        {
            .maxstack 2
            ldarg.0 // this
            ldarg.1 // 'value'
            stfld !0 HW2.Result`1::_value
            ret
        }

        .method public instance !0 get_value() cil managed
        {
            .maxstack 2
            ldarg.0 // this
            ldfld !0 HW2.Result`1::_value
            ret
        }

        .property instance !0 Value()
        {
            .get instance !0 HW2.Result`1::get_value()
            .set instance void HW2.Result`1::set_value(!0)
        }

        .method public rtspecialname specialname void .ctor
        (bool success,
        string message,
        !0 'value') cil managed
        {
            ldarg.0 // this
            ldarg.1 // success
            ldarg.2 // message
            call instance void [HW2]HW2.Result::.ctor(bool, string)

            ldarg.0 // this
            ldarg.3 // 'value'
            stfld !0 HW2.Result::_value

            ret
        }
    }
}