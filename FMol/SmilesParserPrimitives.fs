module FMol.SmilesParserPrimitives

open FParsec
open FMol.Parsers

let isotope:Parser<int32, unit> = pint32Range 0 999

let elementSymbol:Parser<string, unit> = 
    choice [pstring "He"; pstring "Li"; pstring "Be"; pstring "Ne"; pstring "Na"; pstring "Mg"; pstring "Al";
            pstring "Si"; pstring "Cl"; pstring "Ar"; pstring "Ca"; pstring "Sc"; pstring "Ti"; pstring "Cr";
            pstring "Mn"; pstring "Fe"; pstring "Co"; pstring "Ni"; pstring "Cu"; pstring "Zn"; pstring "Ga";
            pstring "Ge"; pstring "As"; pstring "Se"; pstring "Br"; pstring "Kr"; pstring "Rb"; pstring "Sr";
            pstring "Zr"; pstring "Nb"; pstring "Mo"; pstring "Tc"; pstring "Ru"; pstring "Rh"; pstring "Pd";
            pstring "Ag"; pstring "Cd"; pstring "In"; pstring "Sn"; pstring "Sb"; pstring "Te"; pstring "Xe";
            pstring "Cs"; pstring "Ba"; pstring "Hf"; pstring "Ta"; pstring "Re"; pstring "Os"; pstring "Ir";
            pstring "Pt"; pstring "Au"; pstring "Hg"; pstring "Tl"; pstring "Pb"; pstring "Bi"; pstring "Po";
            pstring "At"; pstring "Rn"; pstring "Fr"; pstring "Ra"; pstring "Rf"; pstring "Db"; pstring "Sg";
            pstring "Bh"; pstring "Hs"; pstring "Mt"; pstring "Ds"; pstring "Rg"; pstring "Cn"; pstring "Fl";
            pstring "Lv"; pstring "La"; pstring "Ce"; pstring "Pr"; pstring "Nd"; pstring "Pm"; pstring "Sm";
            pstring "Eu"; pstring "Gd"; pstring "Tb"; pstring "Dy"; pstring "Ho"; pstring "Er"; pstring "Tm";
            pstring "Yb"; pstring "Lu"; pstring "Ac"; pstring "Th"; pstring "Pa"; pstring "Np"; pstring "Pu";
            pstring "Am"; pstring "Cm"; pstring "Bk"; pstring "Cf"; pstring "Es"; pstring "Fm"; pstring "Md";
            pstring "No"; pstring "Lr"; pstring  "H"; pstring  "B"; pstring  "C"; pstring  "N"; pstring  "O";
            pstring  "F"; pstring  "P"; pstring  "S"; pstring  "K"; pstring  "V"; pstring  "Y"; pstring  "I";
            pstring  "W"; pstring  "U"]

let aromaticSymbol:Parser<string, unit> =
    choice [pstring "b"; pstring "c"; pstring "n"; pstring "o"; pstring "p"; pstring "se"; pstring "s"; pstring "as"]

let chiral:Parser<string, unit> =
    let chiralUtil(prefix, min, max) =
        prefixSuffix (pstring prefix) (pint32Range min max)
    let chiralParsers = 
        ["@TH", 1, 2; "@AL", 1, 2; "@SP", 1, 3; "@TB", 1, 99; "@OH", 1, 99] |> List.map chiralUtil
    let chiralShorthandParsers =
        [pstring "@@"; pstring "@"]
    choice (chiralParsers @ chiralShorthandParsers)

let hcount:Parser<int, unit> = pchar 'H' >>. ((digit |>> (string >> int)) <|>% 1)

let charge:Parser<int, unit> =
    let sign:Parser<int->int, unit> =
        (pchar '+' >>. preturn id) <|>
        (pchar '-' >>. preturn ((*)-1))
    parse {
        let! signFunction = sign
        let! n = ((pint32Range 0 15) <|>% 1)
        return signFunction n
    }
