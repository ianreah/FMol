module FMol.Tests.SmilesParserAromaticSymbolTests

open FsCheck
open FsCheck.NUnit

open FMol.Tests.ParserTestHelper
open FMol.Tests.SmilesGenerators
open FMol.SmilesParserPrimitives


[<PropertyAttribute>]
let validAromaticSymbolSucceeds() =
    Prop.forAll (Arb.fromGen aromaticSymbolGenerator) (testParserSucceedsWithInput aromaticSymbol)

[<PropertyAttribute>]
let invalidAromaticSymbolFails() =
    Prop.forAll (Arb.fromGen notAnAromaticSymbolGenerator) (testParserFails aromaticSymbol)