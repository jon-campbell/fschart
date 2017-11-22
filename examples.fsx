#I "packages/FSharp.Charting.0.91.1"
#load "packages/FSharp.Charting.0.91.1/lib/net45/FSharp.Charting.fsx"

open System
open FSharp.Charting

[ for x in 1.0 .. 100.0 -> (x, x ** 2.0) ]
    |> Chart.Line
    |> Chart.Show

[ for i in 0.0 .. 0.02 .. 2.0 * Math.PI -> (sin i, cos i * sin i) ]
    |> Chart.Line
    |> Chart.Show

[ for x in 1.0 .. 100.0 -> x * x * sin x ]
    |> Chart.Line
    |> Chart.Show

let rnd = new Random()
let rand() = rnd.NextDouble()

[ for i in 0 .. 1000 -> rand(), rand() ]
    |> Chart.Point
    |> Chart.Show
