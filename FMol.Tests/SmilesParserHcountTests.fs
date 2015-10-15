module FMol.Tests.SmilesParserHcountTests

open System
open FsCheck
open NUnit.Framework
open FsCheck.NUnit

open FMol.Tests.ParserTestHelper
open FMol.Tests.SmilesGenerators
open FMol.SmilesParserPrimitives

[<Test>]
let validHcountSucceeds() =
    let hs = ("H", 1) :: ([0..9] |> List.map (fun h -> (String.Format("H{0}", h), h)))
    Assert.IsTrue(testParserSucceedsWithAll hcount hs)

[<PropertyAttribute>]
let invalidHcountFails() =
    Prop.forAll (Arb.fromGen invalidhCountGenerator) (testParserFails hcount)