module FMol.Tests.Generators

open FsCheck

let choiceGenerator options = gen {
    let! i = Gen.choose (0, List.length options-1)
    return (List.nth options i)
}

let notAnOptionGenerator options = 
    let startsWithAChoice (s:string) = 
        options |> List.exists (fun x -> s.StartsWith x)
    Gen.suchThat (fun x -> x <> Unchecked.defaultof<string> && not (x |> startsWithAChoice)) Arb.generate<string>