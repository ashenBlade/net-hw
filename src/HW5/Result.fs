module HW5.Result

type Result<'T> =
    | Success of 'T
    | Failure of message: string