module FMol.Tests.SmilesParserAromaticSymbolTests

open FsCheck
open FsCheck.NUnit

open FMol.Tests.ParserTestHelper
open FMol.Tests.Generators
open FMol.SmilesParserPrimitives

let aromaticSymbols = ["b"; "c"; "n"; "o"; "p"; "s"; "se"; "as"]
let aromaticSymbolGenerator = choiceGenerator aromaticSymbols
let notAnAromaticSymbolGenerator = notAnOptionGenerator aromaticSymbols

[<PropertyAttribute>]
let validAromaticSymbolSucceeds() =
    Prop.forAll (Arb.fromGen aromaticSymbolGenerator) (testParserSucceedsWithInput aromaticSymbol)

[<PropertyAttribute>]
let invalidAromaticSymbolFails() =
    Prop.forAll (Arb.fromGen notAnAromaticSymbolGenerator) (testParserFails aromaticSymbol)