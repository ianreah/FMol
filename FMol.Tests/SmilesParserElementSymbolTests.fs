module FMol.Tests.SmilesParserElementSymbolTests

open FsCheck
open NUnit.Framework
open FsCheck.NUnit

open FMol.Tests.ParserTestHelper
open FMol.Tests.SmilesGenerators
open FMol.SmilesParserPrimitives

[<Test>]
let validElementSucceeds() =
    testParserSucceedsWithAllOptions elementSymbol elementSymbols

[<PropertyAttribute>]
let invalidElementFails() =
    Prop.forAll (Arb.fromGen notAnElementSymbolGenerator) (testParserFails elementSymbol)