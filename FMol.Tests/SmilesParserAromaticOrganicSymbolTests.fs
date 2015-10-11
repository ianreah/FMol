module FMol.Tests.SmilesParserAromaticOrganicSymbolTests

open FsCheck
open NUnit.Framework
open FsCheck.NUnit

open FMol.Tests.ParserTestHelper
open FMol.Tests.SmilesGenerators
open FMol.SmilesParserPrimitives

[<Test>]
let validSymbolSucceeds() =
    Assert.IsTrue(testParserSucceedsWithAllOptions aromatic_organic aromaticOrganicSymbols)

[<PropertyAttribute>]
let invalidSymbolFails() =
    Prop.forAll (Arb.fromGen notAnAromaticOrganicSymbolGenerator) (testParserFails aromatic_organic)