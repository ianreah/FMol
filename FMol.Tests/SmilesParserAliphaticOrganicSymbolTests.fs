module FMol.Tests.SmilesParserAliphaticOrganicSymbolTests

open FsCheck
open NUnit.Framework
open FsCheck.NUnit

open FMol.Tests.ParserTestHelper
open FMol.Tests.SmilesGenerators
open FMol.SmilesParserPrimitives

[<Test>]
let validSymbolSucceeds() =
    Assert.IsTrue(testParserSucceedsWithAllOptions aliphatic_organic aliphaticOrganicSymbols)

[<PropertyAttribute>]
let invalidSymbolFails() =
    Prop.forAll (Arb.fromGen notAliphaticOrganicSymbolGenerator) (testParserFails aliphatic_organic)