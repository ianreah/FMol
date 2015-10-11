module FMol.Tests.ParserTestHelper

open FParsec

let private testParser parser input successAction failureAction =
    match run parser input with
    | Success(result, state, position) -> successAction(result, state, position)
    | Failure(message, error, state) -> failureAction(message, error, state)

let private allTestsAreTrue test input =
    input |> List.map test |> List.forall ((=)true)

// The parser should successfully parse the input
// and the result should be equal to 'expected'
let testParserSucceedsWith parser (input, expected) =
    testParser parser input (fun (result, _, _) -> result=expected) (fun _ -> false)

// The parser should successfully parse the input
// and the result should simply be the input string
let testParserSucceedsWithInput parser input =
    testParserSucceedsWith parser (input, input)

// The parser should fail to parse the input
let testParserFails parser input =
    testParser parser input (fun _ -> false) (fun _ -> true)

// The parser should successfully parse each of the input options
// and for each option the result should simply be the input option
let testParserSucceedsWithAllOptions parser options =
    allTestsAreTrue (testParserSucceedsWithInput parser) options