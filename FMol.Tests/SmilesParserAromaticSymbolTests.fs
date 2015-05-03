module SmilesParserAromaticSymbolTests

open FsCheck
open FsCheck.NUnit

open FMol.Tests.ParserTestHelper
open FMol.Tests.Generators
open FMol.SmilesParserPrimitives

let aromaticSymbols = ["b"; "c"; "n"; "o"; "p"; "s"; "se"; "as"]
let aromaticSymbolGenerator = parserInputOutputChoice aromaticSymbols
let notAnAromaticSymbolGenerator = notAnOptionGenerator aromaticSymbols

[<PropertyAttribute>]
let validAromaticSymbolSucceeds() =
    Prop.forAll (Arb.fromGen aromaticSymbolGenerator) (testParserSucceedsWith aromaticSymbol)

[<PropertyAttribute>]
let invalidAromaticSymbolFails() =
    Prop.forAll (Arb.fromGen notAnAromaticSymbolGenerator) (testParserFails aromaticSymbol)