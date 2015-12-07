namespace FMol

type Hydrogens = 
    | Implicit
    | Count of int

type Atom = {
    Isotope: int option
    Symbol: string
    Chiralty: string
    hCount: Hydrogens
    Charge: int
    AtomClass: int option
}