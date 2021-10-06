namespace HW3

module Calculator =
    let Calculate (val1: int) (operation: string) (val2: int):int =
        match operation with
        | "+" -> val1 + val2
        | "-" -> val1 - val2
        | "*" -> val1 * val2
        | "/" -> val1 / val2
        | _ -> 0