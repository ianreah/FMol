module FMol.Tests.ParserTestHelper

open FParsec

let testParser parser input successAction failureAction =
    match run parser input with
    | Success(result, state, position) -> successAction(result, state, position)
    | Failure(message, error, state) -> failureAction(message, error, state)

let testParserSucceedsWith parser (input, expected) =
    testParser parser input (fun (result, _, _) -> result=expected) (fun _ -> false)

let testParserFails parser input =
    testParser parser input (fun _ -> false) (fun _ -> true)