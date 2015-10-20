module FMol.Tests.SmilesGenerators

open System

open FsCheck
open FMol.Tests.GeneratorHelpers

let private digits = ([ '0'..'9' ] |> List.map string)

// A general-purpose SMILES parser must accept at least three digits
// for the isotope and values from 0 to 999
// An isotope is interpreted as a number, so that [2H], [02H] and
// [002H] all mean deuterium.
let validIsotopeGenerator = paddedNumberGenerator (Gen.choose (0, 999))

let outOfRangeIsotopeGenerator = gen {
    let! neg = Arb.generate<bool>
    let! invalidIsotope = Gen.suchThat (fun x -> if neg then x < 0 else x > 999) Arb.generate<int>
    return invalidIsotope.ToString()
} 

let invalidIsotopeGenerator = arbitraryStringExcluding digits

let elementSymbols =
    [ "H"; "He"; "Li"; "Be";  "B";  "C";  "N";  "O";  "F"; "Ne"; "Na"; "Mg";
     "Al"; "Si";  "P";  "S"; "Cl"; "Ar";  "K"; "Ca"; "Sc"; "Ti";  "V"; "Cr";
     "Mn"; "Fe"; "Co"; "Ni"; "Cu"; "Zn"; "Ga"; "Ge"; "As"; "Se"; "Br"; "Kr";
     "Rb"; "Sr";  "Y"; "Zr"; "Nb"; "Mo"; "Tc"; "Ru"; "Rh"; "Pd"; "Ag"; "Cd";
     "In"; "Sn"; "Sb"; "Te";  "I"; "Xe"; "Cs"; "Ba"; "Hf"; "Ta";  "W"; "Re";
     "Os"; "Ir"; "Pt"; "Au"; "Hg"; "Tl"; "Pb"; "Bi"; "Po"; "At"; "Rn"; "Fr";
     "Ra"; "Rf"; "Db"; "Sg"; "Bh"; "Hs"; "Mt"; "Ds"; "Rg"; "Cn"; "Fl"; "Lv";
     "La"; "Ce"; "Pr"; "Nd"; "Pm"; "Sm"; "Eu"; "Gd"; "Tb"; "Dy"; "Ho"; "Er";
     "Tm"; "Yb"; "Lu"; "Ac"; "Th"; "Pa";  "U"; "Np"; "Pu"; "Am"; "Cm"; "Bk";
     "Cf"; "Es"; "Fm"; "Md"; "No"; "Lr"]

let elementSymbolGenerator = chooseFrom elementSymbols
let notAnElementSymbolGenerator = arbitraryStringExcluding elementSymbols

let aromaticSymbols = ["b"; "c"; "n"; "o"; "p"; "s"; "se"; "as"]
let aromaticSymbolGenerator = chooseFrom aromaticSymbols
let notAnAromaticSymbolGenerator = arbitraryStringExcluding aromaticSymbols

let chiralityGenerator = gen {
    let! prefix, min, max = chooseFrom [("@", 0, 0); ("@@", 0, 0); ("@TH", 1, 2); ("@AL", 1, 2); ("@SP", 1, 3); ("@TB", 1, 99); ("@OH", 1, 99)]
    let! n = Gen.choose (min, max)
    if n=0 then return prefix
    else return String.Format("{0}{1}", prefix, n)
}

let hCountGenerator = gen {
    // If H is speciﬁed without a number, it is identical to H1. 
    let! excludeNumber = Arb.generate<bool>
    if excludeNumber then
        return ("H", 1)
    else
        // Hydrogens inside of brackets are speciﬁed as Hn where n is a number such as H3. 
        let! n = Gen.choose (0, 9)
        return (String.Format("H{0}", n), n)
}

let invalidhCountGenerator = arbitraryStringExcluding ["H"]

// Charge is speciﬁed by a +n or -n where n is a number; if the number is missing, it means either +1 or -1 as appropriate.
// An implementation is required to accept charges in the range -15 to +15.
let chargeGenerator = gen {
    let! polarity =  chooseFrom [("+", id); ("-", (*)-1)]
    let! excludeNumber = Arb.generate<bool>
    if excludeNumber then
        return ((fst polarity), (snd polarity) 1)
    else
        let! n = Gen.choose (0, 15)
        return (String.Format("{0}{1}", fst polarity, n), (snd polarity) n)
}

let tooBigChargeGenerator = gen {
    let! n = Gen.suchThat (fun x -> x > 15 || x < -15) Arb.generate<int>
    return string n
}

let invalidChargeGenerator = arbitraryStringExcluding ["+"; "-"]

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
    let! invalidAtomClass = arbitraryStringExcluding ("-"::digits)
    return String.Format(":{0}", invalidAtomClass)
}

let aliphaticOrganicSymbols = ["Cl"; "Br"; "B"; "C"; "N"; "O"; "S"; "P"; "F"; "I"]

let aliphaticOrganicSymbolGenerator = chooseFrom aliphaticOrganicSymbols
let notAliphaticOrganicSymbolGenerator = arbitraryStringExcluding aliphaticOrganicSymbols

let aromaticOrganicSymbols = ["b"; "c"; "n"; "o"; "s"; "p"]

let aromaticOrganicSymbolGenerator = chooseFrom aromaticOrganicSymbols
let notAnAromaticOrganicSymbolGenerator = arbitraryStringExcluding aromaticOrganicSymbols