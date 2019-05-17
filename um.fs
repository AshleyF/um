open System
open System.IO

let input = Console.OpenStandardInput()
let output = Console.OpenStandardOutput()
let mem = new ResizeArray<uint32 array>()
let reg = Array.zeroCreate 8

let rec cycle p =
    let op = mem.[0].[p]
    let a, b, c = int ((op >>> 6) &&& 0b111u), int ((op >>> 3) &&& 0b111u), int ((op) &&& 0b111u)
    match op >>> 28 with
    | 0u -> (if reg.[c] <> 0u then reg.[a] <- reg.[b]); cycle (p + 1)                                   // conditional
    | 1u -> (reg.[a] <- mem.[int reg.[b]].[int reg.[c]]); cycle (p + 1)                                 // load
    | 2u -> (mem.[int reg.[a]].[int reg.[b]] <- reg.[c]); cycle (p + 1)                                 // store
    | 3u -> (reg.[a] <- reg.[b] + reg.[c]); cycle (p + 1)                                               // add
    | 4u -> (reg.[a] <- reg.[b] * reg.[c]); cycle (p + 1)                                               // multiply
    | 5u -> (reg.[a] <- reg.[b] / reg.[c]); cycle (p + 1)                                               // divide
    | 6u -> (reg.[a] <- ~~~ (reg.[b] &&& reg.[c])); cycle (p + 1)                                       // nand
    | 7u -> ()                                                                                          // halt
    | 8u -> (mem.Add(Array.zeroCreate (int reg.[c])); reg.[b] <- uint32 (mem.Count - 1)); cycle (p + 1) // malloc
    | 9u -> (mem.[int reg.[c]] <- [||]); cycle (p + 1)                                                  // free
    | 10u -> (output.WriteByte(byte reg.[c])); cycle (p + 1)                                            // output
    | 11u -> (reg.[c] <- input.ReadByte() |> uint32); cycle (p + 1)                                     // input
    | 12u -> (if reg.[b] <> 0u then mem.[0] <- Array.copy mem.[int reg.[b]]); cycle (int reg.[c])       // program
    | 13u -> (reg.[int (op >>> 25) &&& 0b111] <- op &&& 0b1111111111111111111111111u); cycle (p + 1)    // literal
    | _ -> failwith "Invalid instruction"

// "sandmark.umz"
// "codex.umz"
"umix.um"
|> File.ReadAllBytes |> Array.map uint32 |> Array.chunkBySize 4 |> Array.map (fun b -> (b.[0]<<<24) + (b.[1]<<<16) + (b.[2]<<<8) + b.[3]) |> mem.Add
cycle 0