module FMol.Tests.SmilesParserAromaticSymbolTests

open FsCheck
open NUnit.Framework
open FsCheck.NUnit

open FMol.Tests.ParserTestHelper
open FMol.Tests.SmilesGenerators
open FMol.SmilesParserPrimitives


[<Test>]
let validAromaticSymbolSucceeds() =
    Assert.IsTrue(testParserSucceedsWithAllOptions aromaticSymbol aromaticSymbols)

[<PropertyAttribute>]
let invalidAromaticSymbolFails() =
    Prop.forAll (Arb.fromGen notAnAromaticSymbolGenerator) (testParserFails aromaticSymbol)