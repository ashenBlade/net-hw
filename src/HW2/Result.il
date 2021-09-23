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
    .class public ansi auto Result
        extends [System.Runtime]System.Object
    {
        .field private bool _success
        .field private string _message

        .method private void set_success(bool success) cil managed
        {
            .maxstack 2
            ldarg.0 // this
            ldarg.1 // success
            stfld bool HW2.Result::_success
            ret
        }

        .method public bool get_success() cil managed
        {
            .maxstack 2
            ldarg.0
            ldfld bool HW2.Result::_success
            ret
        }

        .property instance bool Success()
        {
            .get instance bool HW2.Result::get_success()
            .set instance void HW2.Result::set_success(bool)
        }

        .method private void set_failure(bool failure) cil managed
        {
            .maxstack 2
            ldarg.0 // this
            ldarg.1 // success
            neg
            stfld bool HW2.Result::_success
            ret
        }

        .method private bool get_failure() cil managed
        {
            .maxstack 2
            ldarg.0
            ldfld bool HW2.Result::_success
            neg
            ret
        }

        .property instance bool Failure()
        {
            .get instance bool HW2.Result::get_failure()
            .set instance void HW2.Result::set_failure(bool)
        }
        .method private void set_message(string message) cil managed
        {
            .maxstack 2
            ldarg.0 // this
            ldarg.1 // success
            stfld bool HW2.Result::_message
            ret
        }

        .method public string get_message() cil managed
        {
             .maxstack 2
             ldarg.0 // this
             ldfld string HW2.Result::_message
             ret
        }

        .property instance string Message()
        {
            .get instance string HW2.Result::get_message()
            .set instance void HW2.Result::set_message(string)
        }

        .method public hidebysig rtspecialname specialname instance void .ctor
        (bool success,
        string message
        ) cil managed
        {
            .maxstack 8
            ldarg.0 // this
            call instance void [System.Runtime]System.Object::.ctor()

            ldarg.0
            ldarg.1
            stfld bool HW2.Result::_success

            ldarg.0
            ldarg.2
            stfld string HW2.Result::_message

            ret
        }
    }
}