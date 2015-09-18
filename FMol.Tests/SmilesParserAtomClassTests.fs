module SmilesParserAtomClassTests

open System
open FsCheck
open NUnit.Framework
open FsCheck.NUnit

open FMol.Tests.ParserTestHelper
open FMol.Tests.Generators

open FMol.SmilesParserPrimitives

// An "atom class" is an arbitrary integer, a number that has no chemical meaning.
// It is used by applications to mark atoms in ways that are meaningful only to the application.
// Multiple atoms may be labeled with the same atom class.
// The atom class is interpreted as a number, so both [CH2:5] and [NH4+:005] have
// an atom class of 5.
let validAtomClassGenerator = gen {
    let! classNumber = paddedNumberGenerator Arb.generate<int>
    return ":" + (classNumber |> fst), classNumber |> snd;
}

let invalidAtomClassGenerator = gen {
    let! invalidAtomClass = notStartWithGenerator (Array.append [|'-'|] [| '0'..'9' |])
    return String.Format(":{0}", invalidAtomClass)
}

[<PropertyAttribute>]
let validAtomClassSucceeds() =
    Prop.forAll (Arb.fromGen validAtomClassGenerator) (testParserSucceedsWith atomClass)

[<PropertyAttribute>]
let invalidAtomClassFails() =
    Prop.forAll (Arb.fromGen invalidAtomClassGenerator) (testParserFails atomClass)