module FMol.Tests.SmilesParserElementSymbolTests

open FsCheck
open FsCheck.NUnit

open FMol.Tests.ParserTestHelper
open FMol.Tests.SmilesGenerators
open FMol.SmilesParserPrimitives


[<PropertyAttribute>]
let validElementSucceeds() =
    Prop.forAll (Arb.fromGen elementSymbolGenerator) (testParserSucceedsWithInput elementSymbol)

[<PropertyAttribute>]
let invalidElementFails() =
    Prop.forAll (Arb.fromGen notAnElementSymbolGenerator) (testParserFails elementSymbol)