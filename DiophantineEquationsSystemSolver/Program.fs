open System
open Microsoft.FSharp.Core

type vec = float array 
type matrix = float[,]

let n = 1
let m = 2
let line = [|36; 13;|]



//1 0
//4 -13
//-13 36

let min row =
    let absMinCond = fun (x: float) -> x |> Math.Abs
    let projection num = match num with
                            | 0. -> Double.MaxValue
                            | _ -> absMinCond num
    let unpack f (_,b) = f b
    Array.mapi (fun i x -> (i, x)) row |> Array.minBy (unpack projection) |> fst
    
let subtractSeq minuend subtrahend =
    Array.map2 (fun x y -> x - y) minuend subtrahend
     
let subtractColumns (minuend: vec) (subtrahend: vec) targetRow =
    let multiplier = minuend[targetRow] / subtrahend[targetRow]
    let multipliedSubtrahend = Array.map (fun x -> x * multiplier) subtrahend
    subtractSeq minuend multipliedSubtrahend

    
let zeroRow (matrix: matrix) (rowIndex:int) =   
    let offset = rowIndex + 1
    let row = matrix.[rowIndex, *]
    let minValueIndex = (rowIndex, matrix.[rowIndex, *]) ||> Array.skip |>  min 
    let minValueColumn = matrix.[*, minValueIndex]
    let subRowi = Array.mapi (fun i x -> (i, x)) row
    let toSubtract = Array.where (fun (i,_) -> i <> minValueIndex && i > rowIndex) subRowi |> Array.map (fun (i,_) -> matrix.[*,i])
    let subtracted = Array.map (fun (x: vec) -> subtractColumns x minValueColumn rowIndex) toSubtract
    let columnIndexes = Array.where (fun (i,_) -> i <> minValueIndex && i > rowIndex) subRowi |> Array.map fst
    Array.iter (fun (i: int) -> matrix[*, i] <- subtracted[i-offset][*]) columnIndexes    
    
let transform (matrix: matrix) =
    Array.iteri (fun i _ -> zeroRow matrix i) matrix[0,*]

let matrix = array2D [|[|3.;4;0;-8;|];[|7.;0;5;-6;|];[|1;0;0;0|];[|0;1;0;0|];[|0;0;1;0|];|]

//let res = transform matrix
zeroRow matrix 0
// zeroRow matrix 1
// zeroRow matrix 2
printf $"%A{matrix}"

    
    
   