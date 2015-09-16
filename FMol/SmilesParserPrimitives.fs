module FMol.SmilesParserPrimitives

open FParsec
open FMol.Parsers

let isotope:Parser<int32, unit> = pint32Range 0 999

let elementSymbol:Parser<string, unit> = 
    stringChoice ["He"; "Li"; "Be"; "Ne"; "Na"; "Mg"; "Al";
                  "Si"; "Cl"; "Ar"; "Ca"; "Sc"; "Ti"; "Cr";
                  "Mn"; "Fe"; "Co"; "Ni"; "Cu"; "Zn"; "Ga";
                  "Ge"; "As"; "Se"; "Br"; "Kr"; "Rb"; "Sr";
                  "Zr"; "Nb"; "Mo"; "Tc"; "Ru"; "Rh"; "Pd";
                  "Ag"; "Cd"; "In"; "Sn"; "Sb"; "Te"; "Xe";
                  "Cs"; "Ba"; "Hf"; "Ta"; "Re"; "Os"; "Ir";
                  "Pt"; "Au"; "Hg"; "Tl"; "Pb"; "Bi"; "Po";
                  "At"; "Rn"; "Fr"; "Ra"; "Rf"; "Db"; "Sg";
                  "Bh"; "Hs"; "Mt"; "Ds"; "Rg"; "Cn"; "Fl";
                  "Lv"; "La"; "Ce"; "Pr"; "Nd"; "Pm"; "Sm";
                  "Eu"; "Gd"; "Tb"; "Dy"; "Ho"; "Er"; "Tm";
                  "Yb"; "Lu"; "Ac"; "Th"; "Pa"; "Np"; "Pu";
                  "Am"; "Cm"; "Bk"; "Cf"; "Es"; "Fm"; "Md";
                  "No"; "Lr";  "H";  "B";  "C";  "N";  "O";
                   "F";  "P";  "S";  "K";  "V";  "Y";  "I";
                   "W";  "U"]

let aromaticSymbol:Parser<string, unit> =
    stringChoice ["b"; "c"; "n"; "o"; "p"; "se"; "s"; "as"]

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
