module FMol.Tests.SmilesParserChargeTests

open System
open FsCheck
open FsCheck.NUnit

open FMol.Tests.SmilesGenerators
open FMol.Tests.ParserTestHelper
open FMol.SmilesParserPrimitives


[<PropertyAttribute>]
let validChargeSucceeds() =
    Prop.forAll (Arb.fromGen chargeGenerator) (testParserSucceedsWith charge)

[<PropertyAttribute>]
let tooBigChargeFails() =
    Prop.forAll (Arb.fromGen tooBigChargeGenerator) (testParserFails charge)

[<PropertyAttribute>]
let invalidChargeFails() =
    Prop.forAll (Arb.fromGen invalidChargeGenerator) (testParserFails charge)
