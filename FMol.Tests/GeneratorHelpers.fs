module FMol.Tests.GeneratorHelpers

open System
open FsCheck

let choiceGenerator options = gen {
    let! i = Gen.choose (0, List.length options-1)
    return (List.nth options i)
}

let notAnOptionGenerator options = 
    let startsWithAChoice (s:string) = 
        options |> List.exists (fun x -> s.StartsWith x)
    Gen.suchThat (fun x -> x <> Unchecked.defaultof<string> && not (x |> startsWithAChoice)) Arb.generate<string>

let notStartWithGenerator notAllowedAtStart = gen {
    let! s = Gen.suchThat (fun x -> x <> Unchecked.defaultof<string>) Arb.generate<string>
    return s.TrimStart(notAllowedAtStart)
}

let paddedNumberGenerator numberGenerator = gen {
    let! n = numberGenerator

    let! padWithZeros = Arb.generate<bool>
    let! zeroCount = if padWithZeros && n > 0 then Arb.generate<uint32> else (Gen.constant 0u)
    let padded = String.Format("{0}{1}", (String.replicate (int zeroCount) "0"), n)

    return padded, n;
}