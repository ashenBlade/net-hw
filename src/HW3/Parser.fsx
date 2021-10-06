namespace HW3

module Parser =
    let supportedOperations : string list = [ "+"
                                              "-"
                                              "/"
                                              "*" ]
    let TryParseArguments (args: string[]): ParsingResult = 3