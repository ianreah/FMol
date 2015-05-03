module FMol.Tests.SmilesParserElementSymbolTests

open FsCheck
open FsCheck.NUnit

open FMol.Tests.ParserTestHelper
open FMol.SmilesParserPrimitives

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

let elementSymbolGenerator = gen {
    let! i = Gen.choose (0, List.length elementSymbols-1)
    let symbol = (List.nth elementSymbols i)
    return (symbol, symbol)
}

let notAnElementSymbolGenerator =
    let startsWithAnElementSymbol (s:string) =
        elementSymbols |> List.exists (fun x -> s.StartsWith x)
    Gen.suchThat (fun x -> x <> Unchecked.defaultof<string> && not (x |> startsWithAnElementSymbol)) Arb.generate<string>

[<PropertyAttribute>]
let validElementSucceeds() =
    Prop.forAll (Arb.fromGen elementSymbolGenerator) (testParserSucceedsWith elementSymbol)

[<PropertyAttribute>]
let invalidElementFails() =
    Prop.forAll (Arb.fromGen notAnElementSymbolGenerator) (testParserFails elementSymbol)