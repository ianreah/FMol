module FMol.Tests.SmilesParserHcountTests

open System
open FsCheck
open FsCheck.NUnit

open FMol.Tests.ParserTestHelper
open FMol.Tests.SmilesGenerators
open FMol.SmilesParserPrimitives


[<PropertyAttribute>]
let validHcountSucceeds() =
    Prop.forAll (Arb.fromGen hCountGenerator) (testParserSucceedsWith hcount)

[<PropertyAttribute>]
let invalidHcountFails() =
    Prop.forAll (Arb.fromGen invalidhCountGenerator) (testParserFails hcount)