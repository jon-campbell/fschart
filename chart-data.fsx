#I "packages/FSharp.Charting.0.91.1/lib/net45/"
#load "FSharp.Charting.fsx"

open System
open FSharp.Charting

let twoItemListToTuple = function
    | [x;y] -> (x, y)
    | _ -> ("", "")

let splitLine (delimiter:char) (line:string) =
    line.Split [|delimiter|]
        |> Seq.toList
        |> twoItemListToTuple

let durationParse (duration:string) =

    let strip find (text:string) =
        text
            |> Seq.filter (fun character -> character.ToString() <> find)
            |> String.Concat

    let minutesAndSecondsToSeconds = function
        | (minutes, seconds) -> (60 * minutes) + seconds

    duration
        |> strip "m"
        |> strip "s"
        |> splitLine ':'
        |> (fun (x, y) -> (Int32.Parse x, Int32.Parse y))
        |> minutesAndSecondsToSeconds

let stripHeader = function
    | _::xs -> xs
    | x -> x

let Table dataFile delimiter parseColumns chartType =

    let toListOfColumns row = splitLine delimiter row

    dataFile
        |> System.IO.File.ReadLines
        |> Seq.toList
        |> stripHeader
        |> Seq.map (toListOfColumns >> parseColumns)
        |> chartType
        |> Chart.Show

let Frequency dataFile parse chartType =
    dataFile
        |> System.IO.File.ReadLines
        |> Seq.toList
        |> stripHeader
        |> Seq.map parse
        |> Seq.groupBy id
        |> Seq.map (fun (x, g) -> (x, Seq.length g))
        |> chartType
        |> Chart.Show

let DateAndDuration = function
    | x, y -> DateTime.Parse x, durationParse y

let DateAndInt32 = function
    | x, y -> (DateTime.Parse x), (Int32.Parse y)


// Table "robot-duration.txt" '\t' DateAndDuration Chart.Line
// Table "robot-tests.txt" ','  DateAndInt32 Chart.Line
Frequency "pomodoro.txt" DateTime.Parse Chart.Column
